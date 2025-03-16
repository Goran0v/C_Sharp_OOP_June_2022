using System;
using System.Linq;
using System.Collections.Generic;

namespace FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();
            string command;
                while ((command = Console.ReadLine()) != "END")
                {
                    try
                    {
                    string[] cmdArgs = command.Split(";", StringSplitOptions.RemoveEmptyEntries).ToArray();
                    string com = cmdArgs[0];
                    string teamName = cmdArgs[1];
                    if (com == "Team")
                    {
                        Team team = new Team(teamName);
                        teams.Add(team);
                    }
                    else if (com == "Add")
                    {
                        Team team = teams.FirstOrDefault(t => t.Name == teamName);
                        if (team == null)
                        {
                            throw new InvalidOperationException($"Team {teamName} does not exist.");
                        }
                        else
                        {
                            string playerName = cmdArgs[2];
                            int endurance = int.Parse(cmdArgs[3]);
                            int sprint = int.Parse(cmdArgs[4]);
                            int dribble = int.Parse(cmdArgs[5]);
                            int passing = int.Parse(cmdArgs[6]);
                            int shooting = int.Parse(cmdArgs[7]);
                            Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
                            team.AddPlayer(player);
                        }
                    }
                    else if (com == "Remove")
                    {
                        Team team = teams.FirstOrDefault(t => t.Name == teamName);
                        if (team == null)
                        {
                            throw new InvalidOperationException($"Team {teamName} does not exist.");
                        }
                        else
                        {
                            team.RemovePlayer(cmdArgs[2]);
                        }
                    }
                    else if (com == "Rating")
                    {
                        Team team = teams.FirstOrDefault(t => t.Name == teamName);
                        if (team == null)
                        {
                            throw new InvalidOperationException($"Team {teamName} does not exist.");
                        }
                        else
                        {
                            Console.WriteLine(team);
                        }
                    }
                    }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("A name should not be empty.");
                }
            }
                    
        }
    }
}
