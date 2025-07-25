
namespace DevExpress.AI.WinForms.HtmlChat {
    partial class ChatControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.tablePanel = new DevExpress.Utils.Layout.TablePanel();
            this.typingBox = new DevExpress.XtraEditors.HtmlContentControl();
            this.messageEdit = new DevExpress.XtraEditors.MemoEdit();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.messagesItemsView = new DevExpress.XtraGrid.Views.Items.ItemsView();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvatar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.messageMenuPopup = new DevExpress.XtraEditors.HtmlContentPopup(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel)).BeginInit();
            this.tablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typingBox)).BeginInit();
            this.typingBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesItemsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageMenuPopup)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel
            // 
            this.tablePanel.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)});
            this.tablePanel.Controls.Add(this.typingBox);
            this.tablePanel.Controls.Add(this.gridControl);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 1F)});
            this.tablePanel.Size = new System.Drawing.Size(430, 600);
            this.tablePanel.TabIndex = 1;
            // 
            // typingBox
            // 
            this.typingBox.AutoScroll = false;
            this.tablePanel.SetColumn(this.typingBox, 0);
            this.typingBox.Controls.Add(this.messageEdit);
            this.typingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typingBox.Location = new System.Drawing.Point(0, 540);
            this.typingBox.Margin = new System.Windows.Forms.Padding(0);
            this.typingBox.Name = "typingBox";
            this.tablePanel.SetRow(this.typingBox, 1);
            this.typingBox.Size = new System.Drawing.Size(430, 60);
            this.typingBox.TabIndex = 4;
            this.typingBox.ElementMouseClick += new DevExpress.Utils.Html.DxHtmlElementMouseEventHandler(this.TypingBox_ElementMouseClick);
            // 
            // messageEdit
            // 
            this.messageEdit.Location = new System.Drawing.Point(149, 3);
            this.messageEdit.Name = "messageEdit";
            this.messageEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.messageEdit.Properties.Appearance.Options.UseFont = true;
            this.messageEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.messageEdit.Properties.NullValuePrompt = "Type your message here...";
            this.messageEdit.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.messageEdit.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            this.messageEdit.Size = new System.Drawing.Size(100, 96);
            this.messageEdit.TabIndex = 0;
            this.messageEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageEdit_PreviewKeyDown);
            // 
            // gridControl
            // 
            this.tablePanel.SetColumn(this.gridControl, 0);
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.messagesItemsView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl.Name = "gridControl";
            this.tablePanel.SetRow(this.gridControl, 0);
            this.gridControl.Size = new System.Drawing.Size(430, 540);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.messagesItemsView});
            // 
            // messagesItemsView
            // 
            this.messagesItemsView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.messagesItemsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserName,
            this.colAvatar,
            this.colText,
            this.colStatusText});
            this.messagesItemsView.GridControl = this.gridControl;
            this.messagesItemsView.Name = "messagesItemsView";
            this.messagesItemsView.OptionsSelection.AllowContentSelection = DevExpress.Utils.DefaultBoolean.True;
            this.messagesItemsView.QueryItemTemplate += new DevExpress.XtraGrid.Views.Items.QueryItemTemplateEventHandler(this.OnQueryItemTemplate);
            this.messagesItemsView.ElementMouseClick += new DevExpress.XtraGrid.Views.Items.ItemsViewHtmlElementMouseEventHandler(this.OnMessagesViewElementMouseClick);
            // 
            // colUserName
            // 
            this.colUserName.FieldName = "Owner.UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 0;
            // 
            // colAvatar
            // 
            this.colAvatar.FieldName = "Owner.Avatar";
            this.colAvatar.Name = "colAvatar";
            this.colAvatar.Visible = true;
            this.colAvatar.VisibleIndex = 1;
            // 
            // colText
            // 
            this.colText.FieldName = "Text";
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 2;
            // 
            // colStatusText
            // 
            this.colStatusText.FieldName = "StatusText";
            this.colStatusText.Name = "colStatusText";
            this.colStatusText.Visible = true;
            this.colStatusText.VisibleIndex = 3;
            // 
            // messageMenuPopup
            // 
            this.messageMenuPopup.ContainerControl = this;
            this.messageMenuPopup.HideOnElementClick = DevExpress.Utils.DefaultBoolean.True;
            this.messageMenuPopup.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            // 
            // ChatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tablePanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ChatControl";
            this.Size = new System.Drawing.Size(430, 600);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel)).EndInit();
            this.tablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.typingBox)).EndInit();
            this.typingBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesItemsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageMenuPopup)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Items.ItemsView messagesItemsView;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colAvatar;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusText;
        private DevExpress.XtraEditors.HtmlContentControl typingBox;
        private DevExpress.XtraEditors.MemoEdit messageEdit;
        private DevExpress.XtraEditors.HtmlContentPopup messageMenuPopup;
    }
}
