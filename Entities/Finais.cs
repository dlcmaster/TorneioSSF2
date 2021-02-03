using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TorneioSSF2.Entities
{
    class Finais : Torneio
    {
        static string path_finais = path_tmp + @"\finais.txt";
        
        public static void Comecar ()
        {
            while (File.Exists(path_participantes))
            {
                int ondeParou = BatalhaOndeParou();

                switch (ondeParou)
                {
                    case 0:
                        string[] finalistas_pz = File.ReadAllLines(path_participantes);
                        List<Challenger> classificados_pz;
                        List<Challenger> tmp_pz = new List<Challenger>();

                        string[] le_ranking1 = File.ReadAllLines(path_ultimo_ranking);
                        int[] classificacao_challenger1 = new int[finalistas_pz.Length];

                        for (int i = 0; i < finalistas_pz.Length; i++)
                        {
                            for (int j = 0; j < le_ranking1.Length; j++)
                            {
                                if (le_ranking1[j].Split('(')[0] == finalistas_pz[i])
                                    classificacao_challenger1[i] = j + 1;
                            }
                        }

                        Console.WriteLine("--------------------------------------------------------------------");
                        Console.WriteLine("POLYGON ZONE");
                        Console.WriteLine("{0} ({4}º) - {1} ({5}º) - {2} ({6}º) - {3} ({7}º)", finalistas_pz[0], finalistas_pz[1], finalistas_pz[2], finalistas_pz[3], classificacao_challenger1[0], classificacao_challenger1[1], classificacao_challenger1[2], classificacao_challenger1[3]);
                        for (int i = 0; i < finalistas_pz.Length; i++) //recebe as informações da batalha realizada
                        {
                            Console.Write("{0} KO's: ", finalistas_pz[i]);
                            finalistas_pz[i] += ";" + Console.ReadLine();
                            Console.Write("{0} Falls: ", finalistas_pz[i].Split(';')[0]);
                            finalistas_pz[i] += ";" + Console.ReadLine();
                            Console.WriteLine("--------------------------------------------------------------------");

                            string nome = finalistas_pz[i].Split(';')[0];
                            List<int> kos = new List<int>() { int.Parse(finalistas_pz[i].Split(';')[1]) };
                            List<int> falls = new List<int>() { int.Parse(finalistas_pz[i].Split(';')[2]) };

                            tmp_pz.Add(new Challenger(nome, kos, falls));
                        }

                        classificados_pz = Classificados(tmp_pz, 3);

                        Console.WriteLine("Classificados para a Battlefield: ");
                        foreach (Challenger c in classificados_pz)
                            Console.WriteLine(" - {0}", c.Nome);
                        Console.ReadLine();
                        Console.Clear();

                        AtualizaParticipantes(tmp_pz, classificados_pz);
                        break;

                    case 1:
                        string[] finalistas_b = File.ReadAllLines(path_participantes);
                        List<Challenger> classificados_b;
                        List<Challenger> tmp_b = new List<Challenger>();

                        string[] le_ranking2 = File.ReadAllLines(path_ultimo_ranking);
                        int[] classificacao_challenger2 = new int[finalistas_b.Length - 4];

                        for (int i = 4; i < finalistas_b.Length; i++)
                        {
                            for (int j = 0; j < le_ranking2.Length; j++)
                            {
                                if (le_ranking2[j].Split('(')[0] == finalistas_b[i])
                                    classificacao_challenger2[i - 4] = j + 1;
                            }
                        }

                        Console.WriteLine("--------------------------------------------------------------------");
                        Console.WriteLine("BATTLEFIELD");
                        Console.WriteLine("{0} ({3}º) - {1} ({4}º) - {2} ({5}º)", finalistas_b[4], finalistas_b[5], finalistas_b[6], classificacao_challenger2[0], classificacao_challenger2[1], classificacao_challenger2[2]);
                        for (int i = 4; i < finalistas_b.Length; i++) //recebe as informações da batalha realizada
                        {
                            Console.Write("{0} KO's: ", finalistas_b[i]);
                            finalistas_b[i] += ";" + Console.ReadLine();
                            Console.Write("{0} Falls: ", finalistas_b[i].Split(';')[0]);
                            finalistas_b[i] += ";" + Console.ReadLine();
                            Console.WriteLine("--------------------------------------------------------------------");

                            string nome = finalistas_b[i].Split(';')[0];
                            List<int> kos = new List<int>() { int.Parse(finalistas_b[i].Split(';')[1]) };
                            List<int> falls = new List<int>() { int.Parse(finalistas_b[i].Split(';')[2]) };

                            tmp_b.Add(new Challenger(nome, kos, falls));
                        }

                        classificados_b = Classificados(tmp_b, 2);

                        Console.WriteLine("Classificados para a Final Destination: ");
                        foreach (Challenger c in classificados_b)
                            Console.WriteLine(" - {0}", c.Nome);
                        Console.ReadLine();
                        Console.Clear();

                        AtualizaParticipantes(tmp_b, classificados_b);
                        break;

                    case 2:
                        string[] finalistas_fd = File.ReadAllLines(path_participantes);
                        List<Challenger> classificados_fd;
                        List<Challenger> tmp_fd = new List<Challenger>();

                        string[] le_ranking3 = File.ReadAllLines(path_ultimo_ranking);
                        int[] classificacao_challenger3 = new int[finalistas_fd.Length - 7];

                        for (int i = 7; i < finalistas_fd.Length; i++)
                        {
                            for (int j = 0; j < le_ranking3.Length; j++)
                            {
                                if (le_ranking3[j].Split('(')[0] == finalistas_fd[i])
                                    classificacao_challenger3[i - 7] = j + 1;
                            }
                        }

                        Console.WriteLine("--------------------------------------------------------------------");
                        Console.WriteLine("FINAL DESTINATION");
                        Console.WriteLine("{0} ({2}º) - {1} ({3}º)", finalistas_fd[7], finalistas_fd[8], classificacao_challenger3[0], classificacao_challenger3[1]);
                        for (int i = 7; i < finalistas_fd.Length; i++) //recebe as informações da batalha realizada
                        {
                            Console.Write("{0} KO's: ", finalistas_fd[i]);
                            finalistas_fd[i] += ";" + Console.ReadLine();
                            Console.Write("{0} Falls: ", finalistas_fd[i].Split(';')[0]);
                            finalistas_fd[i] += ";" + Console.ReadLine();
                            Console.WriteLine("--------------------------------------------------------------------");

                            string nome = finalistas_fd[i].Split(';')[0];
                            List<int> kos = new List<int>() { int.Parse(finalistas_fd[i].Split(';')[1]) };
                            List<int> falls = new List<int>() { int.Parse(finalistas_fd[i].Split(';')[2]) };

                            tmp_fd.Add(new Challenger(nome, kos, falls));
                        }

                        classificados_fd = Classificados(tmp_fd, 1);
                        AtualizaParticipantes(tmp_fd, classificados_fd);
                        Console.WriteLine("O grande campeão do {0}º Torneio Super Smash Flash: {1}!", Directory.GetFiles(path_edicoes).Length, classificados_fd[0].Nome.ToUpper());
                        Console.ReadLine();
                        Console.Clear();
                        File.AppendAllText(path_edicao_atual, "\n" + classificados_fd[0].Nome);
                        File.Delete(path_participantes);

                        break;
                }
            }
        }

        public static int BatalhaOndeParou ()
        {
            string[] lines = File.ReadAllLines(path_participantes);
            if (lines.Length == 4) return 0;
            else if (lines.Length == 7) return 1;
            else return 2;
        }
    }
}
