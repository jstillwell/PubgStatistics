using PUBGSharp.Net.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PubgStatistics.Models {
  /// <summary>
  /// First item in each List is the k/d ratio 
  /// The second is the number of wins.
  /// Third is top 10 finishes
  /// </summary>
  public class MinimalStats {
    public List<string> Solo { get; set; }
    public List<string> Duos { get; set; }
    public List<string> Squads { get; set; }
  }
}