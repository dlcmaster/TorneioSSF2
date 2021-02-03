using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TorneioSSF2.Entities
{
    class Classificacao : Torneio
    {
        public static void AtualizaClassificacaoGeral ()
        {
            if (File.Exists(path_classificacao_all_time))
            {
                File.Delete(path_classificacao_all_time);
            }

            int n = Directory.GetFiles(path_edicoes).Length;
            List<Challenger> allChallengers = new List<Challenger>();
            string[] nomes_challengers = File.ReadAllLines(path_participantes_original);

            for (int i = 0; i < nomes_challengers.Length; i++)
            {
                Challenger atual = new Challenger(); //para armazenar a pontuação total do challenger
                atual.Nome = nomes_challengers[i];

                for (int j = 1; j <= n; j++)
                {
                    bool primeira_aparicao = true; //para marcar a primeira correspondência do challenger
                    using (StreamReader sr = File.OpenText(path_edicoes + @"/ed" + j.ToString() + ".txt"))
                    {
                        int kos = 0, falls = 0;
                        while (!sr.EndOfStream)
                        {
                            string[] info = sr.ReadLine().Split(';');
                            if (info[0] == nomes_challengers[i] & info.Length == 7)
                            {
                                if (primeira_aparicao)
                                {
                                    kos += int.Parse(info[1]) + int.Parse(info[3]) + int.Parse(info[5]);
                                    falls += int.Parse(info[2]) + int.Parse(info[4]) + int.Parse(info[6]);
                                    primeira_aparicao = false;
                                } else
                                {
                                    kos += int.Parse(info[1]) + int.Parse(info[3]) + int.Parse(info[5]) + 5; //5 pontos de bônus pela classificação
                                    falls += int.Parse(info[2]) + int.Parse(info[4]) + int.Parse(info[6]);
                                }
                            } else if (info[0] == nomes_challengers[i] & info.Length == 3)
                            {
                                kos += int.Parse(info[1]) + 5; //5 pontos de bônus pela classificação
                                falls += int.Parse(info[2]);
                            } else if (info[0] == nomes_challengers[i])
                            {
                                kos += 5;
                            }
                        }
                        if (kos - falls < 0) atual.Pont += 0; //se o challenger não conseguiu somar pontos a pontuação é zero. não há pontuações negativas na classificação all-time
                        else atual.Pont += kos - falls;
                    }
                }
                allChallengers.Add(atual);
            }

            allChallengers.Sort();
            for (int i = allChallengers.Count - 1; i >= 0; i--)
            {
                if (i == allChallengers.Count - 1)
                {
                    File.AppendAllText(path_classificacao_all_time, allChallengers[i].Nome + " (" + allChallengers[i].Pont.ToString() + " pontos)");
                } else
                {
                    File.AppendAllText(path_classificacao_all_time, "\n" + allChallengers[i].Nome + " (" + allChallengers[i].Pont.ToString() + " pontos)");
                }
            }
        }
    }
}
