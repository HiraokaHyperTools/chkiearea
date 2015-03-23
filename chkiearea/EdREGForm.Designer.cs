namespace ChkIEArea {
    partial class EdREGForm {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.PictureBox pictureBox1;
            this.lTodo = new System.Windows.Forms.Label();
            this.flp1 = new System.Windows.Forms.FlowLayoutPanel();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = global::ChkIEArea.Properties.Resources.Gear;
            pictureBox1.Location = new System.Drawing.Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(32, 32);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lTodo
            // 
            this.lTodo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lTodo.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lTodo.Location = new System.Drawing.Point(50, 12);
            this.lTodo.Name = "lTodo";
            this.lTodo.Size = new System.Drawing.Size(627, 32);
            this.lTodo.TabIndex = 1;
            this.lTodo.Text = "レジストリの構造と、変更点を確認します。";
            this.lTodo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flp1
            // 
            this.flp1.AutoSize = true;
            this.flp1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp1.Location = new System.Drawing.Point(12, 50);
            this.flp1.MinimumSize = new System.Drawing.Size(32, 32);
            this.flp1.Name = "flp1";
            this.flp1.Size = new System.Drawing.Size(32, 32);
            this.flp1.TabIndex = 2;
            // 
            // EdREGForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(689, 832);
            this.Controls.Add(this.flp1);
            this.Controls.Add(this.lTodo);
            this.Controls.Add(pictureBox1);
            this.Name = "EdREGForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChkIEArea レジストリの内容について";
            this.Load += new System.EventHandler(this.EdREGForm_Load);
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lTodo;
        private System.Windows.Forms.FlowLayoutPanel flp1;

    }
}