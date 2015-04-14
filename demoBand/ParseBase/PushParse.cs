using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;

namespace demoBand.ParseBase
{
    public class PushParse
    {

        public async static Task sendNotification(string songname, string artistname, string collaborator)
        {
            var push = new ParsePush();
            push.Query = from user in ParseInstallation.Query
                         where user.Get<string>("username") == collaborator
                         select user;
            push.Alert = "Collaboration request\nTitle:\t"+songname+"\n"+"Author:\t"+artistname;

            await push.SendAsync();
        }


        public async static Task notifyAuthor(string songname, string songartist, string collaborator, string instrument) {
            var push = new ParsePush();
            push.Query = from user in ParseInstallation.Query
                         where user.Get<string>("username") == songartist
                         select user;
            push.Alert = "Song: "+songname+" has been changed.\n"+
                         "Collaborator: "+collaborator+" added his/her "+instrument;

            await push.SendAsync();

        }





        public static async Task initializePush(string username) {
            var installation = ParseInstallation.CurrentInstallation;
            installation["user"] = ParseUser.CurrentUser;
            installation["username"] = username;
            await installation.SaveAsync();
        }

    }
}
