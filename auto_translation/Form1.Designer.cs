namespace auto_translation
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.Exit = new System.Windows.Forms.Button();
            this.ComboBox_In = new System.Windows.Forms.ComboBox();
            this.ComboBox_Ou = new System.Windows.Forms.ComboBox();
            this.Change_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(12, 12);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(132, 33);
            this.Exit.TabIndex = 0;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // ComboBox_In
            // 
            this.ComboBox_In.FormattingEnabled = true;
            this.ComboBox_In.Location = new System.Drawing.Point(12, 51);
            this.ComboBox_In.Name = "ComboBox_In";
            this.ComboBox_In.Size = new System.Drawing.Size(121, 20);
            this.ComboBox_In.TabIndex = 3;
            // 
            // ComboBox_Ou
            // 
            this.ComboBox_Ou.FormattingEnabled = true;
            this.ComboBox_Ou.Location = new System.Drawing.Point(13, 78);
            this.ComboBox_Ou.Name = "ComboBox_Ou";
            this.ComboBox_Ou.Size = new System.Drawing.Size(121, 20);
            this.ComboBox_Ou.TabIndex = 4;
            // 
            // Change_Button
            // 
            this.Change_Button.Location = new System.Drawing.Point(141, 64);
            this.Change_Button.Name = "Change_Button";
            this.Change_Button.Size = new System.Drawing.Size(75, 23);
            this.Change_Button.TabIndex = 5;
            this.Change_Button.Text = "Change";
            this.Change_Button.UseVisualStyleBackColor = true;
            this.Change_Button.Click += new System.EventHandler(this.Change_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 221);
            this.Controls.Add(this.Change_Button);
            this.Controls.Add(this.ComboBox_Ou);
            this.Controls.Add(this.ComboBox_In);
            this.Controls.Add(this.Exit);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.ComboBox ComboBox_In;
        private System.Windows.Forms.ComboBox ComboBox_Ou;
        private System.Windows.Forms.Button Change_Button;
    }
}

