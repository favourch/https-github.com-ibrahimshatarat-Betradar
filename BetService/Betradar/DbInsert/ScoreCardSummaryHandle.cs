﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BetService.Classes.DbInsert;
using Npgsql;
using NpgsqlTypes;
using SharedLibrary;
using Sportradar.SDK.FeedProviders.LiveOdds.Common;
using Sportradar.SDK.FeedProviders.LiveOdds.LiveOdds;

namespace BetService.Classes.DbInsert
{
    public class ScoreCardSummaryHandle : Core
    {
        public ScoreCardSummaryHandle(ScoreCardSummaryEventArgs args)
        {
            try
            {
                var common = new Common();
                //Task.Factory.StartNew(() => common.insertMatchDataAllDetails((MatchHeader)args.ScoreCardSummary.EventHeader, null));
                // Task.Factory.StartNew(
                //() =>
                //{
                common.updateDmadYellowRedCard(args.ScoreCardSummary, args.ScoreCardSummary.EventHeader.Id);
               //}
               //, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);

               
            }
            catch (Exception ex)
            {
                SharedLibrary.Logg.logger.Fatal(ex.Message);
            }
        }
       
       
    }
}



