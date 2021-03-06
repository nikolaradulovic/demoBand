﻿using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.ViewModel
{
    public class MySongsView: ViewModelBase
    {
        private static List<SongListItem> mySongs;
        private static List<SongListItem> collaborations;

        public List<SongListItem> MySongs
        {
            get
            {
                return mySongs;
            }

             set
            {
                mySongs = value;
                RaisePropertyChanged("MySongs");
            }
        }

        private string selectedSong;

        public string SelectedSong
        {
            get
            {
                return selectedSong;
            }
            set
            {
                selectedSong = value;
                if (selectedSong != null)
                {
                    //
                }
                RaisePropertyChanged("SelectedSong");
            }

        }

        public List<SongListItem> Collaborations
        {
            get
            {
                return collaborations;
            }

            set
            {
                collaborations = value;
                RaisePropertyChanged("Collaborations");
            }
        }

        private string selectedCollaborations;

        public string SelectedCollaborations
        {
            get
            {
                return selectedCollaborations;
            }
            set
            {
                selectedCollaborations = value;
                if (selectedCollaborations != null)
                {
                    //
                }
                RaisePropertyChanged("SelectedCollaborations");
            }

        }

        public MySongsView() { }

        public static void setMySongs(List<SongListItem> list) 
        {
            mySongs = list;
        }

        public static void setCollaboratorSongs(List<SongListItem> list)
        {
            collaborations = list;
        }

    }
}
