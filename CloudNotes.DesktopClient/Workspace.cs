﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CloudNotes.Infrastructure;
using CloudNotes.DESecurity;

namespace CloudNotes.DesktopClient
{
    /// <summary>
    /// Represents the workspace initialized with a note object.
    /// </summary>
    public class Workspace : INotifyPropertyChanged
    {
        private readonly Crypto crypto = Crypto.CreateDefaultCrypto();
        private Guid id;

        private string title;

        private string content;

        private bool isSaved;

        private DateTime datePublished;

        public Workspace() { }

        public Workspace(dynamic note)
        {
            id = note.ID;
            title = note.Title;
            string encryptedContent = note.Content;
            this.content = string.IsNullOrEmpty(encryptedContent) ? string.Empty : this.crypto.Decrypt(encryptedContent);
            datePublished = note.DatePublished;
            isSaved = true;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid ID
        {
            get
            {
                return id;
            }
            set
            {
                if (id == value)
                {
                    return;
                    
                }
                id = value;
                OnPropertyChanged("ID");
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title == value) return;
                title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                if (this.content == value)
                {
                    return;
                }
                this.isSaved = false;
                this.content = value;
                this.OnPropertyChanged("Content");
            }
        }

        public bool IsSaved
        {
            get
            {
                return isSaved;
            }
            set
            {
                if (isSaved == value) return;
                isSaved = value;
                OnPropertyChanged("IsSaved");
            }
        }

        public DateTime DatePublished
        {
            get
            {
                return datePublished;
            }
            set
            {
                if (datePublished == value) return;
                datePublished = value;
                OnPropertyChanged("DatePublished");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var evnt = PropertyChanged;
            if (evnt != null) evnt(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
