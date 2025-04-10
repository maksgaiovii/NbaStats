﻿namespace NbaStats.DAL.Data;

public class TeamSeasonAverage
{
    public int TeamSeasonAveragesId { get; set; }

    public int TeamId { get; set; }

    public int SeasonId { get; set; }

    public decimal AvgPoints { get; set; }

    public decimal AvgAssists { get; set; }

    public decimal AvgRebounds { get; set; }

    public decimal AvgTurnovers { get; set; }
    
    public decimal AvgSteals { get; set; }
    
    public decimal AvgBlocks { get; set; }

    public virtual required Season Season { get; set; }

    public virtual  required Team Team { get; set; }
}
