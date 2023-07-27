namespace Parallel
{
    partial class MainPar
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            sortButton = new Button();
            parSortButton = new Button();
            sortTimeLabel = new Label();
            parTimeLabel = new Label();
            gangTimeLabel = new Label();
            gangSortButton = new Button();
            forTimeLabel = new Label();
            forSortButton = new Button();
            SuspendLayout();
            // 
            // sortButton
            // 
            sortButton.Location = new Point(33, 22);
            sortButton.Name = "sortButton";
            sortButton.Size = new Size(75, 23);
            sortButton.TabIndex = 0;
            sortButton.Text = "sort";
            sortButton.UseVisualStyleBackColor = true;
            sortButton.Click += sortButton_Click;
            // 
            // parSortButton
            // 
            parSortButton.Location = new Point(33, 51);
            parSortButton.Name = "parSortButton";
            parSortButton.Size = new Size(75, 23);
            parSortButton.TabIndex = 1;
            parSortButton.Text = "sort par";
            parSortButton.UseVisualStyleBackColor = true;
            parSortButton.Click += parSortButton_Click;
            // 
            // sortTimeLabel
            // 
            sortTimeLabel.AutoSize = true;
            sortTimeLabel.Location = new Point(126, 26);
            sortTimeLabel.Name = "sortTimeLabel";
            sortTimeLabel.Size = new Size(34, 15);
            sortTimeLabel.TabIndex = 2;
            sortTimeLabel.Text = "time:";
            // 
            // parTimeLabel
            // 
            parTimeLabel.AutoSize = true;
            parTimeLabel.Location = new Point(126, 55);
            parTimeLabel.Name = "parTimeLabel";
            parTimeLabel.Size = new Size(34, 15);
            parTimeLabel.TabIndex = 3;
            parTimeLabel.Text = "time:";
            parTimeLabel.Click += label1_Click;
            // 
            // gangTimeLabel
            // 
            gangTimeLabel.AutoSize = true;
            gangTimeLabel.Location = new Point(126, 84);
            gangTimeLabel.Name = "gangTimeLabel";
            gangTimeLabel.Size = new Size(34, 15);
            gangTimeLabel.TabIndex = 5;
            gangTimeLabel.Text = "time:";
            // 
            // gangSortButton
            // 
            gangSortButton.Location = new Point(33, 80);
            gangSortButton.Name = "gangSortButton";
            gangSortButton.Size = new Size(75, 23);
            gangSortButton.TabIndex = 4;
            gangSortButton.Text = "sort gang";
            gangSortButton.UseVisualStyleBackColor = true;
            gangSortButton.Click += gangSortButton_Click;
            // 
            // forTimeLabel
            // 
            forTimeLabel.AutoSize = true;
            forTimeLabel.Location = new Point(126, 113);
            forTimeLabel.Name = "forTimeLabel";
            forTimeLabel.Size = new Size(34, 15);
            forTimeLabel.TabIndex = 7;
            forTimeLabel.Text = "time:";
            // 
            // forSortButton
            // 
            forSortButton.Location = new Point(33, 109);
            forSortButton.Name = "forSortButton";
            forSortButton.Size = new Size(75, 23);
            forSortButton.TabIndex = 6;
            forSortButton.Text = "sort for";
            forSortButton.UseVisualStyleBackColor = true;
            forSortButton.Click += forSortButton_Click;
            // 
            // MainPar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(forTimeLabel);
            Controls.Add(forSortButton);
            Controls.Add(gangTimeLabel);
            Controls.Add(gangSortButton);
            Controls.Add(parTimeLabel);
            Controls.Add(sortTimeLabel);
            Controls.Add(parSortButton);
            Controls.Add(sortButton);
            Name = "MainPar";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button sortButton;
        private Button parSortButton;
        private Label sortTimeLabel;
        private Label parTimeLabel;
        private Label gangTimeLabel;
        private Button gangSortButton;
        private Label forTimeLabel;
        private Button forSortButton;
    }
}