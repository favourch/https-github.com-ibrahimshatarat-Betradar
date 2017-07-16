﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Npgsql;
using NpgsqlTypes;
using SharedLibrary;
using Sportradar.SDK.FeedProviders.LiveOdds.Common;
using Sportradar.SDK.FeedProviders.LiveOdds.LiveOdds;

namespace Betradar.Classes.DbInsert
{
    public class BetStopHandle : Core
    {
        public BetStopHandle(BetStopEventArgs args)
        {
            try
            {

                var common = new Common();
                //if (args.BetStop. .Name != null)
                //{
                //    NameDictionary.Add("BET", entity.Name.International);
                //    NameDictionary.Add("en", entity.Name.International);
                //    foreach (var language in entity.Name.AvailableTranslationLanguages)
                //    {
                //        NameDictionary.Add(language, entity.Name.GetTranslation(language));
                //    }
                //}
                // common.insertMatchDataAllDetails((MatchHeader)args.BetStop.EventHeader, null);



                var merch = config.AppSettings.Get("ChannelsSecretPrefixLast_real");
                var channel = CreateLiveOddsChannelName(args.BetStop.EventHeader.Id, "global", merch);
                merch = config.AppSettings.Get("ChannelsSecretPrefixLast_real2");
                var channel2 = CreateLiveOddsChannelName(args.BetStop.EventHeader.Id, "global", merch);


                Task.Factory.StartNew(
                  () =>
                  {
                      SendToHybridgeSocketMessages(args.BetStop.Status.ToString(), channel);
                      SendToHybridgeSocketMessages(args.BetStop.Status.ToString(), channel2);
                  }
                  , CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);

                //Task.Factory.StartNew(() =>
                //{
                //    SendToHybridgeSocketMessages(args.BetStop.Status.ToString(), channel);
                //    SendToHybridgeSocketMessages(args.BetStop.Status.ToString(), channel2);

                //});


                // var channel = CreateLiveOddsChannelName(args.BetStop.EventHeader.Id, "global");
                // SendToHybridgeSocketMessages(args.BetStop.Status.ToString(), channel);

                Task.Factory.StartNew(
                    () =>
                    {
                        common.insertMatchDataAllDetails((MatchHeader) args.BetStop.EventHeader, null);
                    }
                    , CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);




                //Task.Factory.StartNew(() => common.insertMatchDataAllDetails((MatchHeader)args.BetStop.EventHeader, null));
            }
            catch (Exception ex)
            {
                SharedLibrary.Logg.logger.Fatal(ex.Message);
            }
        }


    }
}
