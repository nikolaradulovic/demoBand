using demoBand.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demoBand.ViewModel
{
   public  class HelpView: ViewModelBase
    {
       private List<HelpElement> helpCreateList;
       private List<HelpElement> helpCollaborateList;
       private List<HelpElement> helpDiscoverList;

       public List<HelpElement> HelpCreateList
       {
           get
           {
               return helpCreateList;
           }

           set
           {
               helpCreateList = value;
               RaisePropertyChanged("HelpCreateList");
           }
       }

       public List<HelpElement> HelpCollaborateList
       {
           get
           {
               return helpCollaborateList;
           }

           set
           {
               helpCollaborateList = value;
               RaisePropertyChanged("HelpCollaborateList");
           }
       }

       public List<HelpElement> HelpDiscoverList
       {
           get
           {
               return helpDiscoverList;
           }

           set
           {
               helpDiscoverList = value;
               RaisePropertyChanged("HelpDiscoverList");
           }
       }

       public HelpView()
       {
           helpCreateList = new List<HelpElement>();
           helpCreateList.Add(new HelpElement("Fist click on \"Create\" tab", "../Assets/Images/Backgrounds/login-background.png"));
           helpCreateList.Add(new HelpElement("Then choose your instrument", "../Assets/Images/Backgrounds/login-background.png"));
           helpCreateList.Add(new HelpElement("...and start recording", "../Assets/Images/Backgrounds/login-background.png"));
           helpCreateList.Add(new HelpElement("Save song on the end", "../Assets/Images/Backgrounds/login-background.png"));

           helpCollaborateList = new List<HelpElement>();
           helpCollaborateList.Add(new HelpElement("Fist click on \"My Songs\" tab", "../Assets/Images/Backgrounds/login-background.png"));
           helpCollaborateList.Add(new HelpElement("Find song you want to collaborate on", "../Assets/Images/Backgrounds/login-background.png"));
           helpCollaborateList.Add(new HelpElement("Choose desired instruments", "../Assets/Images/Backgrounds/login-background.png"));
           helpCollaborateList.Add(new HelpElement("...and start collaborating", "../Assets/Images/Backgrounds/login-background.png"));
       }
    }
}
