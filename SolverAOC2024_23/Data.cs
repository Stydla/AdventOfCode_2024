using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2024_23
{
  internal class Data : DataBase
  {


    public HashSet<Computer> AllComputers = new HashSet<Computer>();
    public List<Computer> AllComputersList = new List<Computer>();

    public Data(string input) : base(input)
    {
      foreach(string line in Lines)
      {
        string[] parts = line.Split('-');
        Computer c1 = GetComputer(parts[0]);
        Computer c2 = GetComputer(parts[1]);
        c1.AddConnection(c2);
        c2.AddConnection(c1);
      }
    }

    private Computer GetComputer(string name)
    {
      Computer tmp = new Computer(name);
      if(AllComputers.TryGetValue(tmp, out Computer computer))
      {
        return computer;
      }
      AllComputers.Add(tmp);
      AllComputersList.Add(tmp);
      return tmp;
    }

    public object Solve1()
    {
      int cnt = 0;
      HashSet<ComputerGroup> groups = GetGroupsOf3();
      foreach (ComputerGroup group in groups)
      {
        if(group.Computers.Any(x=>x.Name[0] == 't'))
        {
          cnt++;
        }
      }
      return cnt;
    }

    private HashSet<ComputerGroup> ExtendGroups(HashSet<ComputerGroup> groups)
    {
      HashSet<ComputerGroup> newGroups = new HashSet<ComputerGroup>();
      foreach (ComputerGroup group in groups)
      {
        foreach (Computer c in AllComputersList)
        {
          if(group.Contains(c)) continue;
          if (group.AreAllConnectedTo(c))
          {
            ComputerGroup newGroup = new ComputerGroup(group);
            newGroup.AddComputer(c);
            if(!newGroups.Contains(newGroup))
            {
              newGroups.Add(newGroup);
            }
            continue;
          }
        }
      }
      return newGroups;
    }

    private HashSet<ComputerGroup> GetGroupsOf3()
    {
      HashSet<ComputerGroup> groups = new HashSet<ComputerGroup>();
      for (int i = 0; i < AllComputersList.Count; i++)
      {
        Computer c1 = AllComputersList[i];
        for (int j = i + 1; j < AllComputersList.Count; j++)
        {
          Computer c2 = AllComputersList[j];
          if (!c1.IsConnectedTo(c2)) continue;

          for (int k = j + 1; k < AllComputersList.Count; k++)
          {
            Computer c3 = AllComputersList[k];
            if (!c1.IsConnectedTo(c3)) continue;
            if (!c2.IsConnectedTo(c3)) continue;

            ComputerGroup group = new ComputerGroup( new List<Computer>(){ c1, c2, c3 });
            if(!groups.Contains(group))
            {
              groups.Add(group);
            }
          }
        }
      }
      return groups;
    }



    public object Solve2()
    {
      HashSet<ComputerGroup> groups = GetGroupsOf3();

      while (true)
      {
        HashSet<ComputerGroup> extendedGroups = ExtendGroups(groups);
        if (extendedGroups.Count == 0)
        {
          break;
        }
        groups = extendedGroups;
      }

      var orderedNames = groups.First().Computers.OrderBy(x => x.Name).Select(x => x.Name);
      string result = string.Join(",", orderedNames);
      return result;
    }
  }
}
