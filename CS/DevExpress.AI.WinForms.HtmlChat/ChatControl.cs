namespace DevExpress.AI.WinForms.HtmlChat
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using DevExpress.AIIntegration;
    using DevExpress.AIIntegration.WinForms;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraGrid.Views.Items;
    using DevExpress.XtraSplashScreen;
    using Microsoft.Extensions.AI;
    using Microsoft.Extensions.DependencyInjection;

    [ToolboxItem(true)]
    public partial class ChatControl : XtraUserControl
    {
        BindingList<ChatMessage> messages = new BindingList<ChatMessage>();

        public ChatControl()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                InitializeStyles();
                InitializeBindings();
                InitializeMessageEdit();
            }
        }

        void InitializeBindings()
        {
            gridControl.DataSource = messages;
            messagesItemsView.TopRowPixelChanged += (sender, e) =>
            {
                messageMenuPopup.Hide();
            };
        }
        void InitializeMessageEdit()
        {
            var autoHeightEdit = messageEdit as IAutoHeightControlEx;
            autoHeightEdit.AutoHeightEnabled = true;
            autoHeightEdit.HeightChanged += OnMessageHeightChanged;
        }

        void InitializeStyles()
        {
            Styles.TypingBox.Apply(typingBox);
            Styles.NoMessages.Apply(messagesItemsView.EmptyViewHtmlTemplate);
        }

        async void MessageEdit_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                string text = messageEdit.Text;
                messageEdit.BeginInvoke(new Action(() => messageEdit.Text = string.Empty));
                await SendMessage(text);
            }
        }

        void OnMessageHeightChanged(object sender, EventArgs e)
        {
            var contentSize = typingBox.GetContentSize();
            typingBox.Height = contentSize.Height;
        }

        void OnMessagesViewElementMouseClick(object sender, ItemsViewHtmlElementMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!string.IsNullOrEmpty(messagesItemsView.SelectedText))
                    ShowContextMenu(e);
            }
        }
        void OnQueryItemTemplate(object sender, QueryItemTemplateEventArgs e)
        {
            var message = e.Row as ChatMessage;
            if (message == null)
                return;
            if (message.Role == ChatRole.User)
                Styles.MyMessage.Apply(e.Template);
            else
                Styles.Message.Apply(e.Template);

            string htmlString = Markdig.Markdown.ToHtml(message.Text);
            e.Template.Template = e.Template.Template.Replace("${Text}", htmlString);
        }

        void ShowContextMenu(ItemsViewHtmlElementMouseEventArgs e)
        {
            Styles.ContextMenu.Apply(messageMenuPopup);
            var size = ScaleDPI.ScaleSize(new Size(212, 100));
            var location = new Point(e.X - size.Width / 2, e.Y - size.Height + ScaleDPI.ScaleVertical(8));
            Rectangle screenRect = gridControl.RectangleToScreen(new Rectangle(location, size));
            messageMenuPopup.Show(gridControl, screenRect);
        }

        async void TypingBox_ElementMouseClick(object sender, Utils.Html.DxHtmlElementMouseEventArgs e)
        {
            if (e.ElementId == "btnSend")
            {
                string message = messageEdit.Text;
                messageEdit.BeginInvoke(new Action(() => messageEdit.Text = string.Empty));
                await SendMessage(message);

            }
            if (e.ElementId == "btnRemove")
            {
                ClearMessages();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)
            {
                IChatClient service = AIExtensionsContainerDesktop.Default.GetService<IChatClient>();
                if (service == null)
                {
                    throw new InvalidOperationException("IChatClient service is not registered in AIExtensionsContainerDesktop.");
                }
            }
        }

        public void SetMessages(IEnumerable<ChatMessage> chatMessages)
        {
            if (chatMessages == null)
                throw new ArgumentNullException(nameof(chatMessages));
            messages.Clear();
            foreach (var message in chatMessages)
            {
                if (message == null)
                    continue;
                messages.Add(message);
            }
            if (messages.Count > 0)
                messagesItemsView.MoveLast();
        }

        public void ClearMessages()
        {
            messages.Clear();
        }

        public async Task SendMessage(string userContent)
        {
            if (string.IsNullOrEmpty(userContent))
                return;
            IChatClient service = AIExtensionsContainerDesktop.Default.GetService<IChatClient>();
            messages.Add(new ChatMessage(ChatRole.User, userContent));
            messagesItemsView.MoveLast();
            AIOverlayForm form = new AIOverlayForm();
            var cancellationTokenSource = new CancellationTokenSource();
            form.ShowLoading(this, cancellationTokenSource);
            try
            {
                ChatResponse chatResponse = await service.GetResponseAsync(messages, cancellationToken: cancellationTokenSource.Token);
                messages.AddMessages(chatResponse);
                messagesItemsView.MoveLast();
                form.Close();
                form.Dispose();
            }
            catch (Exception e)
            {
                form.ShowError(this, e.Message, true);
            }
        }

        sealed class Styles
        {
            public static Style ContextMenu = new ContextMenuStyle();
            public static Style Message = new MessageStyle();
            public static Style MyMessage = new MyMessageStyle();
            public static Style NoMessages = new NoMessagesStyle();
            public static Style TypingBox = new TypingBoxStyle();

            //
            sealed class MessageStyle : Style { }
            sealed class MyMessageStyle : Style { }
            sealed class ContextMenuStyle : Style { }
            sealed class TypingBoxStyle : Style { }
            sealed class NoMessagesStyle : Style { }
        }
    }
}