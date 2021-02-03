using System;
using System.IO;
using System.Collections.Generic;

namespace TorneioSSF2.Entities
{
    class RankingAtual : Torneio
    {
        public static void AtualizaRanking()
        {
            int n = Directory.GetFiles(path_edicoes).Length; //obtém o número de edições já realizadas

            if (n >= 3) //se n < 3, ainda não há ranking atual!
            {
                string path_ranking = path_rankings + @"\r" + (n - 2).ToString() + ".txt";

                List<string> list_tmp = new List<string>();

                using (StreamReader participantes = File.OpenText(path_participantes_original))
                {
                    while (!participantes.EndOfStream)
                    {
                        string p = participantes.ReadLine();
                        double pont = 0;

                        for (int i = 0; i < 3; i++)
                        {
                            using (StreamReader ed = File.OpenText(path_edicoes + @"\ed" + (n - i).ToString() + ".txt"))
                            {
                                bool flag = false; //pra marcar a primeira correspondência do challenger
                                while (!ed.EndOfStream)
                                {
                                    string[] line = ed.ReadLine().Split(";");
                                    if (line[0] == p)
                                    {
                                        if (flag) pont += 5;
                                        if (line.Length == 3)
                                        {
                                            pont += double.Parse(line[1]) - double.Parse(line[2]);
                                        }
                                        else if (line.Length == 7)
                                        {
                                            pont += double.Parse(line[1]) - double.Parse(line[2]) + double.Parse(line[3]) - double.Parse(line[4]) + double.Parse(line[5]) - double.Parse(line[6]);
                                        }
                                        flag = true;
                                    }
                                }
                            }
                        }
                        list_tmp.Add(pont.ToString() + ";" + p);
                    }
                }

                int a = 0, b;
                while (a < list_tmp.Count)
                {
                    for (b = a + 1; b < list_tmp.Count; b++)
                    {
                        if (Double.Parse(list_tmp[b].Split(";")[0]) > Double.Parse(list_tmp[a].Split(";")[0]))
                        {
                            string aux = list_tmp[b];
                            list_tmp[b] = list_tmp[a];
                            list_tmp[a] = aux;
                            a = 0;
                            break;
                        }
                    }
                    if (b == list_tmp.Count) a++;
                }

                for (int i = 0; i < list_tmp.Count; i++)
                {
                    if (i == 0) File.AppendAllText(path_ranking, list_tmp[i].Split(";")[1] + "(" + list_tmp[i].Split(";")[0] + " pontos)");
                    else File.AppendAllText(path_ranking, "\n" + list_tmp[i].Split(";")[1] + "(" + list_tmp[i].Split(";")[0] + " pontos)");
                }
            }
        }
    }
}
