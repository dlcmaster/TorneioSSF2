using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace TorneioSSF2.Entities
{
    class Torneio
    {
        public static string path_participantes { get; private set; } = @"C:\Users\davil\Documents\backup\documentos\C#\TorneioSSF2\participantes_ativos.txt";
        public static string path_participantes_original { get; set; } = @"C:\Users\davil\Documents\backup\documentos\C#\TorneioSSF2\participantes.txt";
        public static string path_rankings { get; private set; } = @"C:\Users\davil\Documents\backup\documentos\C#\TorneioSSF2\rankings";
        public static string path_edicoes { get; private set; } = @"C:\Users\davil\Documents\backup\documentos\C#\TorneioSSF2\eds";
        public static string path_edicao_atual { get; set; }
        public static string path_tmp { get; private set; } = @"C:\Users\davil\Documents\backup\documentos\C#\TorneioSSF2\tmp";
        public static string path_ultimo_ranking { get; private set; }

        public static void Start()
        {
            int ondeParou = ContinuaUltimaFase();
            switch (ondeParou)
            {
                case 1:
                    ResgateDuplo.Comecar();
                    ResgateUnico.Comecar();
                    ChaosShrine.Comecar();
                    WaitingRoom.Comecar();
                    Finais.Comecar();
                    RankingAtual.AtualizaRanking();
                    Console.WriteLine("Torneio finalizado.");
                    break;
                case 2:
                    ResgateUnico.Comecar();
                    ChaosShrine.Comecar();
                    WaitingRoom.Comecar();
                    Finais.Comecar();
                    RankingAtual.AtualizaRanking();
                    Console.WriteLine("Torneio finalizado.");
                    break;
                case 3:
                    ChaosShrine.Comecar();
                    WaitingRoom.Comecar();
                    Finais.Comecar();
                    RankingAtual.AtualizaRanking();
                    Console.WriteLine("Torneio finalizado.");
                    break;
                case 4:
                    WaitingRoom.Comecar();
                    Finais.Comecar();
                    RankingAtual.AtualizaRanking();
                    Console.WriteLine("Torneio finalizado.");
                    break;
                case 5:
                    RankingAtual.AtualizaRanking();
                    Console.WriteLine("Torneio finalizado.");
                    break;
                case 6:
                    Finais.Comecar();
                    RankingAtual.AtualizaRanking();
                    Console.WriteLine("Torneio finalizado.");
                    break;
            }
        }

        public static int ContinuaUltimaFase()
        {
            int num_grupos = Directory.GetFiles(path_tmp).Length;
            switch (num_grupos)
            {
                case 0:
                    if (File.Exists(path_participantes))
                    {
                        path_edicao_atual = path_edicoes + @"\ed" + Directory.GetFiles(path_edicoes).Length.ToString() + ".txt";
                        path_ultimo_ranking = path_rankings + @"\r" + (Directory.GetFiles(path_edicoes).Length - 3).ToString() + ".txt";
                        return 6;
                    }
                    Console.WriteLine("Atualmente não há um torneio em andamento.");
                    Console.Write("Deseja iniciar um novo torneio[s/n]? ");
                    string op = Console.ReadLine();
                    if (op != "n" & op != "N")
                    {
                        File.Create(path_edicoes + @"\ed" + (Directory.GetFiles(path_edicoes).Length + 1).ToString() + ".txt");
                        path_edicao_atual = path_edicoes + @"\ed" + Directory.GetFiles(path_edicoes).Length.ToString() + ".txt";
                        path_ultimo_ranking = path_rankings + @"\r" + (Directory.GetFiles(path_edicoes).Length - 3).ToString() + ".txt";
                        Console.WriteLine("\nNovo torneio iniciado!");
                        Console.WriteLine("Seja bem-vindo ao {0}º Torneio Super Smash Flash!", Directory.GetFiles(path_edicoes).Length);
                        Preliminares.Comecar();
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine("Saindo do programa.");
                        break;
                    }
                case 14:
                    path_edicao_atual = path_edicoes + @"\ed" + Directory.GetFiles(path_edicoes).Length.ToString() + ".txt";
                    path_ultimo_ranking = path_rankings + @"\r" + (Directory.GetFiles(path_edicoes).Length - 3).ToString() + ".txt";
                    Console.WriteLine("Está em andamento o {0}º Torneio Super Smash Flash", Directory.GetFiles(path_edicoes).Length);
                    Preliminares.Comecar();
                    return 1;
                case 7:
                    path_edicao_atual = path_edicoes + @"\ed" + Directory.GetFiles(path_edicoes).Length.ToString() + ".txt";
                    path_ultimo_ranking = path_rankings + @"\r" + (Directory.GetFiles(path_edicoes).Length - 3).ToString() + ".txt";
                    Console.WriteLine("Está em andamento o {0}º Torneio Super Smash Flash", Directory.GetFiles(path_edicoes).Length);
                    ResgateDuplo.Comecar();
                    return 2;
                case 4:
                    path_edicao_atual = path_edicoes + @"\ed" + Directory.GetFiles(path_edicoes).Length.ToString() + ".txt";
                    path_ultimo_ranking = path_rankings + @"\r" + (Directory.GetFiles(path_edicoes).Length - 3).ToString() + ".txt";
                    Console.WriteLine("Está em andamento o {0}º Torneio Super Smash Flash", Directory.GetFiles(path_edicoes).Length);
                    ResgateUnico.Comecar();
                    return 3;
                case 3:
                    path_edicao_atual = path_edicoes + @"\ed" + Directory.GetFiles(path_edicoes).Length.ToString() + ".txt";
                    path_ultimo_ranking = path_rankings + @"\r" + (Directory.GetFiles(path_edicoes).Length - 3).ToString() + ".txt";
                    Console.WriteLine("Está em andamento o {0}º Torneio Super Smash Flash", Directory.GetFiles(path_edicoes).Length);
                    ChaosShrine.Comecar();
                    return 4;
                case 2:
                    path_edicao_atual = path_edicoes + @"\ed" + Directory.GetFiles(path_edicoes).Length.ToString() + ".txt";
                    path_ultimo_ranking = path_rankings + @"\r" + (Directory.GetFiles(path_edicoes).Length - 3).ToString() + ".txt";
                    Console.WriteLine("Está em andamento o {0}º Torneio Super Smash Flash", Directory.GetFiles(path_edicoes).Length);
                    WaitingRoom.Comecar();
                    return 5;
                case 1:
                    path_edicao_atual = path_edicoes + @"\ed" + Directory.GetFiles(path_edicoes).Length.ToString() + ".txt";
                    path_ultimo_ranking = path_rankings + @"\r" + (Directory.GetFiles(path_edicoes).Length - 3).ToString() + ".txt";
                    Console.WriteLine("Está em andamento o {0}º Torneio Super Smash Flash", Directory.GetFiles(path_edicoes).Length);
                    Finais.Comecar();
                    return 5;
            }
            return 0;
        }

        public static void GeraGrupos(int num_participantes, int num_grupos, int part_grupo)
        {
            while (true)
            {
                bool sorteioValido = true;
                Random r = new Random();
                string[] participantes = File.ReadAllLines(path_participantes);
                string[] shuffled = participantes.OrderBy(x => r.Next()).ToArray(); //reordena os participantes ativos de forma aleatória

                if (Directory.GetFiles(path_rankings).Length >= 3)
                {
                    if (num_grupos == 14 || num_grupos == 7 || num_grupos == 4)
                    {
                        string[] elite4 = GetElite4();
                        for (int i = 0; i < num_participantes; i += part_grupo)
                        {
                            if (elite4.Contains(shuffled[i]) & elite4.Contains(shuffled[i + 1]) & elite4.Contains(shuffled[i + 2]))
                            //verifica se todos os membros de um mesmo grupo sorteado são membros da elite 4, o que não é permitido
                            {
                                sorteioValido = false;
                            }
                        }
                    }
                }

                if (sorteioValido)
                {
                    int a = 0, b = 1;
                    while (a < num_participantes)
                    //cria um arquivo para cada grupo sorteado no padrão csv
                    {
                        string text = "0";
                        for (int i = 0; i < part_grupo; i++)
                        {
                            text += "\n" + shuffled[a++];
                        }
                        File.AppendAllText(path_tmp + @"\g" + b.ToString() + ".txt", text);
                        b++;
                    }
                    break;
                }
            }
        }

        public static string[] GetElite4 ()
        {
            string[] elite4 = new string[4];

            using (StreamReader sr = File.OpenText(path_rankings + @"\r" + Directory.GetFiles(path_rankings).Length.ToString() + ".txt"))
            {
                //obtém os nomes dos atuais integrantes da elite 4 e os armazena no vetor elite4[]
                elite4[0] = sr.ReadLine().Split(' ')[0];
                elite4[1] = sr.ReadLine().Split(' ')[0];
                elite4[2] = sr.ReadLine().Split(' ')[0];
                elite4[3] = sr.ReadLine().Split(' ')[0];
            }

            return elite4;
        }

        public static void AtualizaParticipantes(List<Challenger> participantes_do_grupo, List<Challenger> classificados)
        {
            if (File.Exists(path_participantes))
            {
                foreach (Challenger c in classificados)
                {
                    File.AppendAllText(path_participantes, "\n" + c.Nome);
                }
            }
            else
            {
                for (int i = 0; i < classificados.Count; i++)
                {
                    if (i == 0) File.WriteAllText(path_participantes, classificados[i].Nome);
                    else File.AppendAllText(path_participantes, "\n" + classificados[i].Nome);
                }
            }

            if (File.Exists(path_edicao_atual))
            {
                foreach (Challenger c in participantes_do_grupo)
                {
                    if (c.KOs.Count == 3)
                    {
                        if (File.ReadAllLines(path_edicao_atual).Length == 0)
                        {
                            File.AppendAllText(path_edicao_atual, c.Nome + ";" + c.KOs[0].ToString() + ";" + c.Falls[0].ToString() + ";" + c.KOs[1].ToString() + ";" + c.Falls[1].ToString() + ";" + c.KOs[2].ToString() + ";" + c.Falls[2].ToString());
                        } else
                        {
                            File.AppendAllText(path_edicao_atual, "\n" + c.Nome + ";" + c.KOs[0].ToString() + ";" + c.Falls[0].ToString() + ";" + c.KOs[1].ToString() + ";" + c.Falls[1].ToString() + ";" + c.KOs[2].ToString() + ";" + c.Falls[2].ToString());
                        }
                    } else
                    {
                        File.AppendAllText(path_edicao_atual, "\n" + c.Nome + ";" + c.KOs[0].ToString() + ";" + c.Falls[0].ToString());
                    }
                    
                }
            }
            else
            {
                for (int i = 0; i < participantes_do_grupo.Count; i++)
                {
                    if (i == 0)
                    {
                        if (participantes_do_grupo[i].KOs.Count == 3)
                        {
                            File.WriteAllText(path_edicao_atual, participantes_do_grupo[i].Nome + ";" + participantes_do_grupo[i].KOs[0].ToString() + ";" + participantes_do_grupo[i].Falls[0].ToString() + ";" + participantes_do_grupo[i].KOs[1].ToString() + ";" + participantes_do_grupo[i].Falls[1].ToString() + ";" + participantes_do_grupo[i].KOs[2].ToString() + ";" + participantes_do_grupo[i].Falls[2].ToString());
                        } else
                        {
                            File.WriteAllText(path_edicao_atual, participantes_do_grupo[i].Nome + ";" + participantes_do_grupo[i].KOs[0].ToString() + ";" + participantes_do_grupo[i].Falls[0].ToString());
                        }
                    }
                    else
                    {
                        if (participantes_do_grupo[i].KOs.Count == 3)
                        {
                            File.AppendAllText(path_edicao_atual, "\n" + participantes_do_grupo[i].Nome + ";" + participantes_do_grupo[i].KOs[0].ToString() + ";" + participantes_do_grupo[i].Falls[0].ToString() + ";" + participantes_do_grupo[i].KOs[1].ToString() + ";" + participantes_do_grupo[i].Falls[1].ToString() + ";" + participantes_do_grupo[i].KOs[2].ToString() + ";" + participantes_do_grupo[i].Falls[2].ToString());
                        } else
                        {
                            File.AppendAllText(path_edicao_atual, "\n" + participantes_do_grupo[i].Nome + ";" + participantes_do_grupo[i].KOs[0].ToString() + ";" + participantes_do_grupo[i].Falls[0].ToString());
                        }
                        
                    }
                }
            }
        }

        public static int? GrupoOndeParou(int num_grupos)
        {
            int num_batalhas = 0;

            if (Directory.GetFiles(path_tmp).Length == 0) return -1;
            else
            {
                for (int i = 1; i <= num_grupos; i++)
                {
                    string[] text = File.ReadAllLines(path_tmp + @"\g" + i.ToString() + ".txt");
                    if (i == 1) num_batalhas = int.Parse(text[0]);
                    else if (i == num_grupos)
                    {
                        if (int.Parse(text[0]) < num_batalhas) return i;
                        else return 1;
                    }
                    else
                    {
                        if (int.Parse(text[0]) < num_batalhas) return i;
                    }
                }
            }

            return null;
        }

        public static List<Challenger> Classificados (List<Challenger> lista, int num_classificados)
        {
            List<Challenger> classificados = new List<Challenger>();
            List<Challenger> lista_copia = new List<Challenger>();
            foreach (Challenger c in lista)
                lista_copia.Add(c);
            List<int> ponts = new List<int>();
            int max_pont;

            foreach (Challenger c in lista_copia)
                ponts.Add(c.Pont); //pega as pontuações de todos os participantes
            
            while (classificados.Count != num_classificados)
            {
                max_pont = ponts.Max();

                for (int i = 0; i < ponts.Count; i++) 
                    if (ponts[i] == max_pont) classificados.Add(lista_copia[i]);
                ponts.RemoveAll(x => x == max_pont);
                lista_copia.RemoveAll(x => x.Pont == max_pont);

                while (classificados.Count > num_classificados)
                {
                    int menor_ko = int.MaxValue;
                    
                    foreach (Challenger c in classificados)
                        if (c.Pont == max_pont & c.KOs.Sum() < menor_ko) menor_ko = c.KOs.Sum();

                    if (classificados.Count - classificados.FindAll(x => x.Pont == max_pont & x.KOs.Sum() == menor_ko).Count == num_classificados)
                    {
                        classificados.RemoveAll(x => x.Pont == max_pont & x.KOs.Sum() == menor_ko);
                        return classificados;
                    } else if (classificados.Count - classificados.FindAll(x => x.Pont == max_pont & x.KOs.Sum() == menor_ko).Count > num_classificados)
                    {
                        classificados.RemoveAll(x => x.Pont == max_pont & x.KOs.Sum() == menor_ko);
                    } else
                    {
                        List<Challenger> empatados = classificados.FindAll(x => x.Pont == max_pont);
                        while (classificados.Count > num_classificados)
                        {
                            List<int> desempate = new List<int>();
                            Console.Write("STOCK BATTLE!");
                            foreach (Challenger c in empatados)
                            {
                                Console.Write(" - {0}", c.Nome);
                            }
                            Console.WriteLine("");

                            for (int i = 0; i < empatados.Count; i++)
                            {
                                Console.Write("{0} pontuação: ", empatados[i].Nome);
                                desempate.Add(int.Parse(Console.ReadLine()));
                            }
                            if (classificados.Count - desempate.FindAll(x => x == desempate.Min()).Count >= num_classificados)
                            {
                                for (int i = 0; i < desempate.Count; i++)
                                    if (desempate[i] == desempate.Min())
                                        classificados.Remove(empatados[i]);
                            }
                            else
                            {
                                for (int i = 0; i < desempate.Count; i++)
                                    if (desempate[i] != desempate.Min())
                                        empatados.RemoveAt(i);
                            }
                        }
                    }
                }
            }

            return classificados;
        }
    }
}
