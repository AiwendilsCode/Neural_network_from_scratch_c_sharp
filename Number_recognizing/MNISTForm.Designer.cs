
namespace Number_recognizing
{
    partial class MNISTForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trainButton = new System.Windows.Forms.Button();
            this.accuracyTextBox = new System.Windows.Forms.TextBox();
            this.numPicture = new System.Windows.Forms.PictureBox();
            this.compNumber = new System.Windows.Forms.TextBox();
            this.TestAllButton = new System.Windows.Forms.Button();
            this.TestAccuracy = new System.Windows.Forms.TextBox();
            this.fileButton = new System.Windows.Forms.Button();
            this.nextTrainButton = new System.Windows.Forms.Button();
            this.prevTrainButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.testNextButton = new System.Windows.Forms.Button();
            this.testPrevButton = new System.Windows.Forms.Button();
            this.desiredText = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // trainButton
            // 
            this.trainButton.Location = new System.Drawing.Point(43, 29);
            this.trainButton.Name = "trainButton";
            this.trainButton.Size = new System.Drawing.Size(160, 61);
            this.trainButton.TabIndex = 0;
            this.trainButton.Text = "Train";
            this.trainButton.UseVisualStyleBackColor = true;
            this.trainButton.Click += new System.EventHandler(this.trainButton_Click);
            // 
            // accuracyTextBox
            // 
            this.accuracyTextBox.Location = new System.Drawing.Point(281, 39);
            this.accuracyTextBox.Name = "accuracyTextBox";
            this.accuracyTextBox.Size = new System.Drawing.Size(144, 22);
            this.accuracyTextBox.TabIndex = 2;
            // 
            // numPicture
            // 
            this.numPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numPicture.Location = new System.Drawing.Point(248, 75);
            this.numPicture.Name = "numPicture";
            this.numPicture.Size = new System.Drawing.Size(540, 363);
            this.numPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.numPicture.TabIndex = 3;
            this.numPicture.TabStop = false;
            // 
            // compNumber
            // 
            this.compNumber.Location = new System.Drawing.Point(146, 268);
            this.compNumber.Name = "compNumber";
            this.compNumber.Size = new System.Drawing.Size(42, 22);
            this.compNumber.TabIndex = 6;
            // 
            // TestAllButton
            // 
            this.TestAllButton.Location = new System.Drawing.Point(43, 340);
            this.TestAllButton.Name = "TestAllButton";
            this.TestAllButton.Size = new System.Drawing.Size(155, 57);
            this.TestAllButton.TabIndex = 7;
            this.TestAllButton.Text = "Test all";
            this.TestAllButton.UseVisualStyleBackColor = true;
            this.TestAllButton.Click += new System.EventHandler(this.TestAllButton_Click);
            // 
            // TestAccuracy
            // 
            this.TestAccuracy.Location = new System.Drawing.Point(458, 39);
            this.TestAccuracy.Name = "TestAccuracy";
            this.TestAccuracy.Size = new System.Drawing.Size(145, 22);
            this.TestAccuracy.TabIndex = 8;
            // 
            // fileButton
            // 
            this.fileButton.Location = new System.Drawing.Point(623, 12);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(165, 49);
            this.fileButton.TabIndex = 9;
            this.fileButton.Text = "Weights from file";
            this.fileButton.UseVisualStyleBackColor = true;
            this.fileButton.Click += new System.EventHandler(this.fileButton_Click);
            // 
            // nextTrainButton
            // 
            this.nextTrainButton.Location = new System.Drawing.Point(127, 147);
            this.nextTrainButton.Name = "nextTrainButton";
            this.nextTrainButton.Size = new System.Drawing.Size(76, 29);
            this.nextTrainButton.TabIndex = 11;
            this.nextTrainButton.Text = "next";
            this.nextTrainButton.UseVisualStyleBackColor = true;
            this.nextTrainButton.Click += new System.EventHandler(this.nextTrainButton_Click);
            // 
            // prevTrainButton
            // 
            this.prevTrainButton.Location = new System.Drawing.Point(43, 147);
            this.prevTrainButton.Name = "prevTrainButton";
            this.prevTrainButton.Size = new System.Drawing.Size(76, 29);
            this.prevTrainButton.TabIndex = 11;
            this.prevTrainButton.Text = "prev";
            this.prevTrainButton.UseVisualStyleBackColor = true;
            this.prevTrainButton.Click += new System.EventHandler(this.prevTrainButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(43, 119);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 15);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "Train images";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(46, 198);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(154, 15);
            this.textBox3.TabIndex = 13;
            this.textBox3.Text = "Test images";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // testNextButton
            // 
            this.testNextButton.Location = new System.Drawing.Point(127, 219);
            this.testNextButton.Name = "testNextButton";
            this.testNextButton.Size = new System.Drawing.Size(76, 29);
            this.testNextButton.TabIndex = 11;
            this.testNextButton.Text = "next";
            this.testNextButton.UseVisualStyleBackColor = true;
            this.testNextButton.Click += new System.EventHandler(this.testNextButton_Click);
            // 
            // testPrevButton
            // 
            this.testPrevButton.Location = new System.Drawing.Point(43, 219);
            this.testPrevButton.Name = "testPrevButton";
            this.testPrevButton.Size = new System.Drawing.Size(76, 29);
            this.testPrevButton.TabIndex = 11;
            this.testPrevButton.Text = "prev";
            this.testPrevButton.UseVisualStyleBackColor = true;
            this.testPrevButton.Click += new System.EventHandler(this.testPrevButton_Click);
            // 
            // desiredText
            // 
            this.desiredText.Location = new System.Drawing.Point(67, 268);
            this.desiredText.Name = "desiredText";
            this.desiredText.Size = new System.Drawing.Size(38, 22);
            this.desiredText.TabIndex = 14;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Location = new System.Drawing.Point(43, 296);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(86, 15);
            this.textBox5.TabIndex = 15;
            this.textBox5.Text = "Desired";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(135, 296);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(68, 15);
            this.textBox4.TabIndex = 16;
            this.textBox4.Text = "Computed";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Location = new System.Drawing.Point(281, 18);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(144, 15);
            this.textBox6.TabIndex = 17;
            this.textBox6.Text = "Train accuracy";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Location = new System.Drawing.Point(458, 18);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(145, 15);
            this.textBox7.TabIndex = 18;
            this.textBox7.Text = "Test accuracy";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MNISTForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.desiredText);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.testPrevButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.prevTrainButton);
            this.Controls.Add(this.testNextButton);
            this.Controls.Add(this.nextTrainButton);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.TestAccuracy);
            this.Controls.Add(this.TestAllButton);
            this.Controls.Add(this.compNumber);
            this.Controls.Add(this.numPicture);
            this.Controls.Add(this.accuracyTextBox);
            this.Controls.Add(this.trainButton);
            this.Name = "MNISTForm";
            this.Text = "MNIST";
            ((System.ComponentModel.ISupportInitialize)(this.numPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button trainButton;
        private System.Windows.Forms.TextBox accuracyTextBox;
        private System.Windows.Forms.PictureBox numPicture;
        private System.Windows.Forms.TextBox compNumber;
        private System.Windows.Forms.Button TestAllButton;
        private System.Windows.Forms.TextBox TestAccuracy;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.Button nextTrainButton;
        private System.Windows.Forms.Button prevTrainButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button testNextButton;
        private System.Windows.Forms.Button testPrevButton;
        private System.Windows.Forms.TextBox desiredText;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
    }
}

