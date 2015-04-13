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

            //push.Query = from installation in ParseInstallation.Query
            //             where installation.Get<bool>("injuryReports") == true
            //             select installation;
            //push.Alert = "Willie Hayes injured by own pop fly.";
             //var query = from song in ParseObject.GetQuery("Record")
             //           where (song.Get<string>("collaborator") == username
             //              && song.Get<string>("songartist") != username)
             //           select song;
            await push.SendAsync();
        }

        public static async Task initializePush(string username) {
            var installation = ParseInstallation.CurrentInstallation;
            installation["username"] = username;
            await installation.SaveAsync();
        }

    }
}
