using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_09
{
  internal class Data : DataBase
  {


    List<Block> Blocks = new List<Block>();

   

    public Data(string input) : base(input)
    {
      // optionaly parse data

      bool empty = false;
      int id = 0;
      for(int i = 0; i < input.Length; i++)
      {
        char value = input[i];
        int count = (int)value - '0';

        for(int j = 0; j < count; j++)
        {
          Block b = new Block(-1);

          if (!empty)
          {
            b.ID = id;
          }
          Blocks.Add(b);
        }
        if(!empty)
        {
          id++;
        }
        empty = !empty;
      }
    }

    public long CheckSum()
    {
      long sum = 0;
      for (int i = 0; i < Blocks.Count; i++)
      {
        if(Blocks[i].IsEmpty)
        {
          continue;
        }
        sum += (i * Blocks[i].ID);
      }
      return sum;
    }

    public object Solve1()
    {
      int sourceBlockIndex = Blocks.Count - 1;
      for (int i = 0; i < Blocks.Count; i++)
      {
        if(i >= sourceBlockIndex)
        {
          break;
        }

        Block b = Blocks[i];
        if (b.IsEmpty)
        {
          Blocks[i] = Blocks[sourceBlockIndex];
          Blocks[sourceBlockIndex] = new Block(-1);
          while(Blocks[sourceBlockIndex].IsEmpty)
          {
            sourceBlockIndex--;
          }
        }
      }
      
      return CheckSum();
    }

    public object Solve2()
    {
      List<File> Empty = new List<File>();
      List<File> Files = new List<File>();

      for (int i = 0; i < Blocks.Count; i++)
      {
        File fileTmp = GetFile(i);
        if (fileTmp.IsEmpty)
        {
          Empty.Add(fileTmp);
        }
        else
        {
          Files.Add(fileTmp);
        }
        i = fileTmp.EndIndex;
      }

      Files.Reverse();

      for(int i = 0; i < Files.Count; i++)
      {
        File file = Files[i];
        for (int j = 0; j < Empty.Count; j++)
        {
          File empty = Empty[j];

          if (empty.EndIndex >= file.StartIndex) break;

          if(empty.Length >= file.Length)
          {
            for(int k = 0; k < file.Length; k++)
            {
              Blocks[empty.StartIndex + k] = new Block(file.ID);
              Blocks[file.StartIndex + k] = new Block(-1);
            }
            empty.StartIndex += file.Length;
            break;
          }
        }
      }

      return CheckSum();
    }

    private File GetFile(int index)
    {
      File f = new File();
      f.StartIndex = index;
      f.ID = Blocks[index].ID;
      while(index < Blocks.Count && f.ID == Blocks[index].ID)
      {
        index++;
      }
      f.EndIndex = index -1;
      return f;
    }


  



  }
}
