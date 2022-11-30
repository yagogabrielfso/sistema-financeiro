using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Model;
using static System.Console;
using Persist;
using Db;
using ConsoleTables;

namespace SistemFinanceiroSoN
{
    class Program
    {

        private List<Account> accounts;
        private List<Category> categories;

        private AccountDAL account;
        private CategoryDAL categoria;

        public Program()
        {
            string strConn = Db.Connection.GetStringConnection();
            this.account = new AccountDAL(new SqlConnection(strConn));
            this.categoria = new CategoryDAL(new SqlConnection(strConn));
        }
        static void Main(string[] args)
        {
    
            int opc;

            Program p = new Program();

            do
            {
                Title = "CONTROLE FINANCEIRO";
                Uteis.MontaMenu();

                opc = Convert.ToInt32(ReadLine());

                if ( !(opc > 0 && opc <= 6) )
                {
                    Clear();
                    BackgroundColor = ConsoleColor.Red;
                    ForegroundColor = ConsoleColor.White;
                    WriteLine("\n");
                    Uteis.MontaHeader("INFORME UMA OPÇÃO VÁLIDA!", '!', 30);
                    WriteLine("\n");

                    ResetColor();
                }
                else
                {
                    Clear();
                    switch (opc)
                    {
                        case 1:
                            Title = "LISTAGEM DE CONTAS - CONTROLE FINANCEIRO";
                            Uteis.MontaHeader("LISTAGEM DE CONTAS");
                            p.accounts = p.account.List();

                            ConsoleTable table = new ConsoleTable("ID", "Descrição", "Tipo", "Valor");
                            foreach (var c in p.accounts)
                            {
                                table.AddRow(c.Id, c.Description, c.Type.Equals('R') ? "Receber": "Pagar", String.Format("{0:c}", c.Value));
                            }

                            table.Write();
                            ReadLine();
                            Clear();
                            break;
                        case 2:
                            Title = "NOVA CONTA- CONTROLE FINANCEIRO";
                            Uteis.MontaHeader("CADASTRANDO UMA NOVA CONTA");

                            string _description;
                            char _type = ' ';
                            decimal _value = -1.00m;
                            bool _contaCriada = false;
                            DateTime _dueDate = Convert.ToDateTime("01/01/0001");
                            do
                            {
                                Write("Informe a descrição da conta: ");
                                _description = ReadLine();

                                if (_description.Equals(""))
                                {
                                    BackgroundColor = ConsoleColor.Red;
                                    ForegroundColor = ConsoleColor.White;
                                    Uteis.MontaHeader("INFORME UMA DESCRIÇÃO PARA A CONTA", '*', 30);
                                    ResetColor();
                                }
                            } while (_description.Equals(""));

                            do
                            {

                                Write("Informe o tipo da conta P ou R: ");
                                switch (ReadLine().ToUpper())
                                {
                                    case "R":
                                        _type = 'R';
                                        break;
                                    case "P":
                                        _type = 'P';
                                        break;
                                    default:
                                        BackgroundColor = ConsoleColor.Red;
                                        ForegroundColor = ConsoleColor.White;
                                        Uteis.MontaHeader("INFORME UM TIPO PARA A CONTA - P ou R", '*', 30);
                                        ResetColor();
                                        _type = ' ';
                                        break;
                                }
                            } while (_type.Equals(' '));

                            do
                            {
                               
                                Write("Informe o valor da conta: ");

                                string input = ReadLine();
                                string pattern = @"^\d+";
                                Match m = Regex.Match(input, pattern);
                                if (m.Success)
                                {
                                    _value = Convert.ToDecimal(input);
                                }
                                else
                                {
                                    BackgroundColor = ConsoleColor.Red;
                                    ForegroundColor = ConsoleColor.White;
                                    Uteis.MontaHeader("INFORME UM VALOR PARA A CONTA", '*', 30);
                                    ResetColor();
                                }
                              


                            } while (_value == -1.00m);

                            
                            do
                            {
                                Write("Informe a data de vencimento no formato dd/mm/aaaa: ");
                                string input = ReadLine();
                                string pattern = "^(((0[1-9]|[12][0-9]|30)[-/]?(0[13-9]|1[012])|31[-/]?(0[13578]|1[02])|(0[1-9]|1[0-9]|2[0-8])[-/]?02)[-/]?[0-9]{4}|29[-/]?02[-/]?([0-9]{2}(([2468][048]|[02468][48])|[13579][26])|([13579][26]|[02468][048]|0[0-9]|1[0-6])00))$";
                                Match m = Regex.Match(input, pattern);
                                if (m.Success)
                                {
                                    _dueDate = Convert.ToDateTime(input);
                                }
                                else
                                {
                                    BackgroundColor = ConsoleColor.Red;
                                    ForegroundColor = ConsoleColor.White;
                                    Uteis.MontaHeader("INFORME UMA DATA PARA A CONTA ", '*', 30);
                                    ResetColor();
                                }

                            } while (_dueDate == Convert.ToDateTime("01/01/0001"));


                             
                            break;
                        case 3:
                            Write("EDITAR");
                            break;
                        case 4:
                            Write("EXCLUIR");
                            break;
                        case 5:
                            Write("RELATÓRIO");
                            break;
                    }
                   
                }



            } while (opc != 6);

            ReadLine();

    }
        }
}
