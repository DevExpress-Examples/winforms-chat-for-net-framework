namespace DevExpress.AI.WinForms.HtmlChat {
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using DevExpress.AIIntegration;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraGrid.Views.Items;
    using Microsoft.Extensions.AI;
    using Microsoft.Extensions.DependencyInjection;

    [ToolboxItem(true)]
    public partial class ChatControl : XtraUserControl {
        BindingList<ChatMessage> messages = new BindingList<ChatMessage>();
        public ChatControl() {
            InitializeComponent();
            if(!DesignMode) {
                InitializeStyles();
                InitializeBindings();
                InitializeMessageEdit();
            }
        }

        void InitializeStyles() {
            Styles.TypingBox.Apply(typingBox);
            Styles.NoMessages.Apply(messagesItemsView.EmptyViewHtmlTemplate);
        }
        void InitializeBindings() {
            gridControl.DataSource = messages;
            messagesItemsView.TopRowPixelChanged += (sender, e) => {
                messageMenuPopup.Hide();
            };
        }

        async void MessageEdit_PreviewKeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) {
                e.Handled = true;
                string text = messageEdit.Text;
                messageEdit.BeginInvoke(new Action(() => messageEdit.Text = string.Empty));
                await SendMessage(text);
            }
        }

        async void TypingBox_ElementMouseClick(object sender, Utils.Html.DxHtmlElementMouseEventArgs e) {
            if(e.ElementId == "btnSend") {
                await SendMessage(messageEdit.Text);
            }
            if(e.ElementId == "btnRemove") {
                messages.Clear();
            }
        }

        async Task SendMessage(string userContent) {
            if(string.IsNullOrEmpty(userContent))
                return;
            IChatClient service = AIExtensionsContainerDesktop.Default.GetService<IChatClient>();
            messages.Add(new ChatMessage(ChatRole.User, userContent));
            messagesItemsView.MoveLast();
            ChatResponse chatResponse = await service.GetResponseAsync(messages);
            messages.AddMessages(chatResponse);
            messagesItemsView.MoveLast();
        }

        void OnMessagesViewElementMouseClick(object sender, ItemsViewHtmlElementMouseEventArgs e) {
            if(e.Button == MouseButtons.Right) {
                if(!string.IsNullOrEmpty(messagesItemsView.SelectedText))
                    ShowContextMenu(e);
            }
        }

        void ShowContextMenu(ItemsViewHtmlElementMouseEventArgs e) {
            Styles.ContextMenu.Apply(messageMenuPopup);
            var size = ScaleDPI.ScaleSize(new Size(212, 100));
            var location = new Point(e.X - size.Width / 2, e.Y - size.Height + ScaleDPI.ScaleVertical(8));
            Rectangle screenRect = gridControl.RectangleToScreen(new Rectangle(location, size));
            messageMenuPopup.Show(gridControl, screenRect);
        }
        void InitializeMessageEdit() {
            var autoHeightEdit = messageEdit as IAutoHeightControlEx;
            autoHeightEdit.AutoHeightEnabled = true;
            autoHeightEdit.HeightChanged += OnMessageHeightChanged;
        }
        void OnMessageHeightChanged(object sender, EventArgs e) {
            var contentSize = typingBox.GetContentSize();
            typingBox.Height = contentSize.Height;
        }
        void OnQueryItemTemplate(object sender, QueryItemTemplateEventArgs e) {
            var message = e.Row as ChatMessage;
            if(message == null)
                return;
            if(message.Role == ChatRole.User)
                Styles.MyMessage.Apply(e.Template);
            else
                Styles.Message.Apply(e.Template);
            var md = new HeyRed.MarkdownSharp.Markdown();
            string htmlString = md.Transform(message.Text);
            e.Template.Template = e.Template.Template.Replace("${Text}", htmlString);
        }
        sealed class Styles {
            public static Style Message = new MessageStyle();
            public static Style MyMessage = new MyMessageStyle();
            public static Style NoMessages = new NoMessagesStyle();
            public static Style ContextMenu = new ContextMenuStyle();
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