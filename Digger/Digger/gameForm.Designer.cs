namespace Digger
{
    partial class gameForm
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPoints = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.pictureBoxGuy = new System.Windows.Forms.PictureBox();
            this.labelGuy = new System.Windows.Forms.Label();
            this.pictureBoxMissile = new System.Windows.Forms.PictureBox();
            this.pictureBoxBomb = new System.Windows.Forms.PictureBox();
            this.pictureBoxInvicloak = new System.Windows.Forms.PictureBox();
            this.pictureBoxBonus = new System.Windows.Forms.PictureBox();
            this.labelMissile = new System.Windows.Forms.Label();
            this.labelBomb = new System.Windows.Forms.Label();
            this.labelInvicloak = new System.Windows.Forms.Label();
            this.labelBonus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGuy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMissile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBomb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInvicloak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBonus)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(12, 77);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(860, 472);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.Location = new System.Drawing.Point(9, 13);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(80, 20);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "username";
            // 
            // labelPoints
            // 
            this.labelPoints.AutoSize = true;
            this.labelPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPoints.Location = new System.Drawing.Point(95, 13);
            this.labelPoints.Name = "labelPoints";
            this.labelPoints.Size = new System.Drawing.Size(18, 20);
            this.labelPoints.TabIndex = 2;
            this.labelPoints.Text = "0";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLevel.Location = new System.Drawing.Point(9, 52);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(74, 20);
            this.labelLevel.TabIndex = 3;
            this.labelLevel.Text = "Poziom 1";
            // 
            // pictureBoxGuy
            // 
            this.pictureBoxGuy.Location = new System.Drawing.Point(154, 12);
            this.pictureBoxGuy.Name = "pictureBoxGuy";
            this.pictureBoxGuy.Size = new System.Drawing.Size(49, 52);
            this.pictureBoxGuy.TabIndex = 4;
            this.pictureBoxGuy.TabStop = false;
            // 
            // labelGuy
            // 
            this.labelGuy.AutoSize = true;
            this.labelGuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGuy.Location = new System.Drawing.Point(209, 27);
            this.labelGuy.Name = "labelGuy";
            this.labelGuy.Size = new System.Drawing.Size(18, 20);
            this.labelGuy.TabIndex = 5;
            this.labelGuy.Text = "3";
            // 
            // pictureBoxMissile
            // 
            this.pictureBoxMissile.Location = new System.Drawing.Point(278, 12);
            this.pictureBoxMissile.Name = "pictureBoxMissile";
            this.pictureBoxMissile.Size = new System.Drawing.Size(49, 52);
            this.pictureBoxMissile.TabIndex = 6;
            this.pictureBoxMissile.TabStop = false;
            // 
            // pictureBoxBomb
            // 
            this.pictureBoxBomb.Location = new System.Drawing.Point(409, 12);
            this.pictureBoxBomb.Name = "pictureBoxBomb";
            this.pictureBoxBomb.Size = new System.Drawing.Size(49, 52);
            this.pictureBoxBomb.TabIndex = 7;
            this.pictureBoxBomb.TabStop = false;
            // 
            // pictureBoxInvicloak
            // 
            this.pictureBoxInvicloak.Location = new System.Drawing.Point(553, 12);
            this.pictureBoxInvicloak.Name = "pictureBoxInvicloak";
            this.pictureBoxInvicloak.Size = new System.Drawing.Size(49, 52);
            this.pictureBoxInvicloak.TabIndex = 8;
            this.pictureBoxInvicloak.TabStop = false;
            // 
            // pictureBoxBonus
            // 
            this.pictureBoxBonus.Location = new System.Drawing.Point(689, 12);
            this.pictureBoxBonus.Name = "pictureBoxBonus";
            this.pictureBoxBonus.Size = new System.Drawing.Size(49, 52);
            this.pictureBoxBonus.TabIndex = 9;
            this.pictureBoxBonus.TabStop = false;
            // 
            // labelMissile
            // 
            this.labelMissile.AutoSize = true;
            this.labelMissile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMissile.Location = new System.Drawing.Point(333, 27);
            this.labelMissile.Name = "labelMissile";
            this.labelMissile.Size = new System.Drawing.Size(18, 20);
            this.labelMissile.TabIndex = 10;
            this.labelMissile.Text = "0";
            // 
            // labelBomb
            // 
            this.labelBomb.AutoSize = true;
            this.labelBomb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBomb.Location = new System.Drawing.Point(464, 27);
            this.labelBomb.Name = "labelBomb";
            this.labelBomb.Size = new System.Drawing.Size(18, 20);
            this.labelBomb.TabIndex = 11;
            this.labelBomb.Text = "0";
            // 
            // labelInvicloak
            // 
            this.labelInvicloak.AutoSize = true;
            this.labelInvicloak.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvicloak.Location = new System.Drawing.Point(608, 27);
            this.labelInvicloak.Name = "labelInvicloak";
            this.labelInvicloak.Size = new System.Drawing.Size(18, 20);
            this.labelInvicloak.TabIndex = 12;
            this.labelInvicloak.Text = "0";
            // 
            // labelBonus
            // 
            this.labelBonus.AutoSize = true;
            this.labelBonus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBonus.Location = new System.Drawing.Point(744, 27);
            this.labelBonus.Name = "labelBonus";
            this.labelBonus.Size = new System.Drawing.Size(18, 20);
            this.labelBonus.TabIndex = 13;
            this.labelBonus.Text = "0";
            // 
            // gameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.labelBonus);
            this.Controls.Add(this.labelInvicloak);
            this.Controls.Add(this.labelBomb);
            this.Controls.Add(this.labelMissile);
            this.Controls.Add(this.pictureBoxBonus);
            this.Controls.Add(this.pictureBoxInvicloak);
            this.Controls.Add(this.pictureBoxBomb);
            this.Controls.Add(this.pictureBoxMissile);
            this.Controls.Add(this.labelGuy);
            this.Controls.Add(this.pictureBoxGuy);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.labelPoints);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.pictureBox);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "gameForm";
            this.Text = "Digger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.gameForm_FormClosing);
            this.Load += new System.EventHandler(this.gameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGuy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMissile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBomb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInvicloak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBonus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPoints;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.PictureBox pictureBoxGuy;
        private System.Windows.Forms.Label labelGuy;
        private System.Windows.Forms.PictureBox pictureBoxMissile;
        private System.Windows.Forms.PictureBox pictureBoxBomb;
        private System.Windows.Forms.PictureBox pictureBoxInvicloak;
        private System.Windows.Forms.PictureBox pictureBoxBonus;
        private System.Windows.Forms.Label labelMissile;
        private System.Windows.Forms.Label labelBomb;
        private System.Windows.Forms.Label labelInvicloak;
        private System.Windows.Forms.Label labelBonus;
    }
}