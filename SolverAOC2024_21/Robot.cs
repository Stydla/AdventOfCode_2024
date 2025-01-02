using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_21
{


  internal class Cache
  {
    public List<RobotCacheItem> Items = new List<RobotCacheItem>();
    public long Value { get; set; } = 0;

    public void Apply()
    {
      foreach (var item in Items)
      {
        item.Robot.CurrentPosition = item.Position;
      }
    }

    public Cache(Robot robot, long value)
    {
      Robot tmp = robot;
      while (tmp != null)
      {
        Items.Add(new RobotCacheItem() { Robot = tmp, Position = tmp.CurrentPosition });
        tmp = tmp.Input;
      }
      Value = value;
    }
  }

  internal class RobotCacheItem
  {
    public Robot Robot { get; set; }
    public char Position { get; set; }
  }

  internal class Robot
  {

    public Robot Input { get; set; }
    public char CurrentPosition { get; set; } 

    public Dictionary<string, Cache> Cache = new Dictionary<string, Cache>();


    private string GetCacheKey(char input)
    {
      if (Input != null)
      {
        return $"({input}{CurrentPosition}){Input.GetCacheKey(input)}";
      }
      else
      {
        return CurrentPosition.ToString();
      }
    }

    public Dictionary<string, string> MoveMap { get; set; } = new Dictionary<string, string>();

    public long MoveAndAccept(char input)
    {
      string cacheKeyFrom = GetCacheKey(input);
      if (Cache.ContainsKey(cacheKeyFrom))
      {
        Cache[cacheKeyFrom].Apply();
        return Cache[cacheKeyFrom].Value;

      }

      long move = Move(input);
      long accept = Accept(input);
      long res = move + accept;

      Cache cacheTmp = new Cache(this, res);
      if (!Cache.ContainsKey(cacheKeyFrom))
      {
        Cache.Add(cacheKeyFrom, cacheTmp);
      }

      return res;
    }


    public long Solve(string input)
    {
      long res = 0;
      for (int i = 0; i < input.Length; i++)
      {
        char c = input[i];
        long tmp = MoveAndAccept(c);
        res += tmp;
      }
      
      return res;
    }

    public long Move(char input)
    {
      if (Input == null)
      {
        return 0;
      }



      long res = 0;

      string key = $"{CurrentPosition}{input}";
      string path = MoveMap[key];

      StringBuilder sb = new StringBuilder();
      foreach (var c in path)
      {
        long tmp = Input.MoveAndAccept(c);
        res += tmp;
      }

      CurrentPosition = input;

     

      return res;
    }

    public long Accept(char c)
    {
      if (Input == null)
      {
        return 1;
      }

      long move = Input.Move('A');
      long accept = Input.Accept('A');
      return move + accept;
    }



    public Robot()
    {
      CurrentPosition = 'A';

      MoveMap.Add("AA", "");
      MoveMap.Add("vv", "");
      MoveMap.Add("^^", "");
      MoveMap.Add("<<", "");
      MoveMap.Add(">>", "");


      MoveMap.Add("A^", "<");
      MoveMap.Add("Av", "<v");
      MoveMap.Add("A>", "v");
      MoveMap.Add("A<", "v<<");

      MoveMap.Add("^A", ">");
      MoveMap.Add("^>", "v>");
      MoveMap.Add("^v", "v");
      MoveMap.Add("^<", "v<");

      MoveMap.Add(">^", "<^");
      MoveMap.Add(">A", "^");
      MoveMap.Add(">v", "<");
      MoveMap.Add("><", "<<");

      MoveMap.Add("v^", "^");
      MoveMap.Add("vA", "^>");
      MoveMap.Add("v<", "<");
      MoveMap.Add("v>", ">");

      MoveMap.Add("<^", ">^");
      MoveMap.Add("<A", ">>^");
      MoveMap.Add("<v", ">");
      MoveMap.Add("<>", ">>");
    }



  }
}
