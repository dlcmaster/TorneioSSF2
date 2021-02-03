using System;
using System.IO;
using System.Collections.Generic;

namespace TorneioSSF2.Entities
{
    class ResgateDuplo : Torneio
    {
        public static int num_participantes { get; private set; } = 28;
        public static int num_grupos { get; private set; } = 7;
        public static int part_grupo { get; private set; } = 4;

        public static void Comecar()
        {
            int? grupo = GrupoOndeParou(num_grupos);
            if (grupo == -1) //significa que a fase nem começou ainda
            {
                GeraGrupos(num_participantes, num_grupos, part_grupo);
                Console.WriteLine("\nEntrando na Fase de Resgate Duplo");
                Console.WriteLine("Grupos aleatoriamente gerados.");
                File.Delete(path_participantes);
                grupo = 1;
            }

            while (true) //só acaba quando a fase termina ou o programa é encerrado abruptamente
            {
                string[] texto = File.ReadAllLines(path_tmp + @"\g" + grupo.ToString() + ".txt");
                string[] le_ranking = File.ReadAllLines(path_ultimo_ranking);
                int[] classificacao_challenger = new int[texto.Length - 1];

                for (int i = 1; i < texto.Length; i++)
                {
                    for (int j = 0; j < le_ranking.Length; j++)
                    {
                        if (le_ranking[j].Split('(')[0] == texto[i].Split(';')[0])
                            classificacao_challenger[i - 1] = j + 1;
                    }
                }

                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("FASE DE RESGATE DUPLO - Grupo {0} - Rodada {1}", grupo, int.Parse(texto[0]) + 1);
                Console.WriteLine("{0} ({4}º) - {1} ({5}º) - {2} ({6}º) - {3} ({7}º)", texto[1].Split(';')[0], texto[2].Split(';')[0], texto[3].Split(';')[0], texto[4].Split(';')[0], classificacao_challenger[0], classificacao_challenger[1], classificacao_challenger[2], classificacao_challenger[3]);
                for (int i = 1; i <= part_grupo; i++) //recebe as informações da batalha realizada
                {
                    Console.Write("{0} KO's: ", texto[i].Split(';')[0]);
                    texto[i] += ";" + Console.ReadLine();
                    Console.Write("{0} Falls: ", texto[i].Split(';')[0]);
                    texto[i] += ";" + Console.ReadLine();
                    Console.WriteLine("--------------------------------------------------------------------");
                }

                for (int i = 0; i < texto.Length; i++) //grava as informações da batalha no arquivo do grupo e incrementa o contador de batalhas
                {
                    if (i == 0)
                    {
                        int num_rodadas = int.Parse(texto[i]);
                        texto[i] = (num_rodadas + 1).ToString() + "\n";
                        File.WriteAllText(path_tmp + @"\g" + grupo.ToString() + ".txt", texto[i]);
                    }
                    else if (i != texto.Length - 1)
                    {
                        texto[i] += "\n";
                        File.AppendAllText(path_tmp + @"\g" + grupo.ToString() + ".txt", texto[i]);
                    }
                    else
                    {
                        File.AppendAllText(path_tmp + @"\g" + grupo.ToString() + ".txt", texto[i]);
                    }
                }

                if (texto[0] == "3\n") //grupo encerrado, hora de computar os classificados do grupo
                {
                    List<Challenger> participantes_do_grupo = new List<Challenger>();
                    for (int i = 1; i < texto.Length; i++)
                    {
                        string nome = texto[i].Split(';')[0];
                        List<int> KOs = new List<int>() { int.Parse(texto[i].Split(';')[1]), int.Parse(texto[i].Split(';')[3]), int.Parse(texto[i].Split(';')[5]) };
                        List<int> Falls = new List<int>() { int.Parse(texto[i].Split(';')[2]), int.Parse(texto[i].Split(';')[4]), int.Parse(texto[i].Split(';')[6]) };
                        participantes_do_grupo.Add(new Challenger(nome, KOs, Falls));
                    }
                    List<Challenger> classificados = Classificados(participantes_do_grupo, 2);

                    Console.WriteLine("Grupo finalizado. Classificados: ");
                    foreach (Challenger c in classificados)
                        Console.WriteLine(" - {0}", c.Nome);
                    Console.ReadLine();
                    Console.Clear();

                    AtualizaParticipantes(participantes_do_grupo, classificados);
                } else Console.Clear();

                if (grupo == num_grupos & int.Parse(texto[0]) == 3) //se a fase terminou, hora de computar os resgatados
                {
                    List<Challenger> eliminados = new List<Challenger>();
                    string[] array_nomes_classificados = File.ReadAllLines(path_participantes);
                    List<string> list_nomes_classificados = new List<string>();

                    foreach (string nome in array_nomes_classificados)
                        list_nomes_classificados.Add(nome);

                    for (int i = 1; i <= num_grupos; i++)
                    {
                        using (StreamReader sr = File.OpenText(path_tmp + @"\g" + i.ToString() + ".txt"))
                        {
                            sr.ReadLine();
                            for (int j = 0; j < part_grupo; j++)
                            {
                                string line = sr.ReadLine();
                                string nome = line.Split(';')[0];
                                List<int> kos = new List<int>() { int.Parse(line.Split(';')[1]), int.Parse(line.Split(';')[3]), int.Parse(line.Split(';')[5]) };
                                List<int> falls = new List<int>() { int.Parse(line.Split(';')[2]), int.Parse(line.Split(';')[4]), int.Parse(line.Split(';')[6]) };
                                eliminados.Add(new Challenger(nome, kos, falls));
                            }
                        }
                    }

                    eliminados.RemoveAll(x => list_nomes_classificados.Contains(x.Nome));
                    Console.WriteLine("=== RESULTADO DO RESGATE ===");
                    List<Challenger> resgatados = Classificados(eliminados, 2);
                    Console.WriteLine("Challengers resgatados: ");
                    foreach (Challenger c in resgatados)
                    {
                        Console.WriteLine(" - {0}", c.Nome);
                        File.AppendAllText(path_participantes, "\n" + c.Nome);
                    }
                    Console.ReadLine();
                    Console.Clear();

                    for (int i = 1; i <= num_grupos; i++)
                        File.Delete(path_tmp + @"\g" + i.ToString() + ".txt");

                    break;
                }
                else if (grupo == num_grupos)
                    grupo = 1;
                else
                    grupo++;
            }
        }
    }
}
