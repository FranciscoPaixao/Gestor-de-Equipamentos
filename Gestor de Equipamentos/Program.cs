using System.Collections;
using System.Globalization;

namespace Gestor_de_Equipamentos
{
    internal class Program
    {
        private static Dictionary<int, string> equipamentoNome = new Dictionary<int, string>();
        private static Dictionary<int, string> equipamentoNumeroSerie = new Dictionary<int, string>();
        private static Dictionary<int, decimal> equipamentoPrecoAquisicao = new Dictionary<int, decimal>();
        private static Dictionary<int, long> equipamentoDataFabricacao = new Dictionary<int, long>();
        private static Dictionary<int, string> equipamentoFabricante = new Dictionary<int, string>();
        private static Dictionary<int, string> chamadoTitulo = new Dictionary<int, string>();
        private static Dictionary<int, string> chamadoDescricao = new Dictionary<int, string>();
        private static Dictionary<int, int> chamadoEquipamento = new Dictionary<int, int>();
        private static Dictionary<int, long> chamadoDataAbertura = new Dictionary<int, long>();

        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("  1 - Gerenciar Equipamentos");
            Console.WriteLine("  2 - Gerenciar Chamados");
            Console.WriteLine("  3 - Fechar Programa");
            int opcao;

            Console.Write("Opção: ");
            opcao = Convert.ToInt32(Console.ReadLine());
            switch (opcao)
            {
                case 1:
                    MenuEquipamentos();
                    break;
                case 2:
                    MenuChamados();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Programa encerrado!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    MenuPrincipal();
                    Console.WriteLine("Informe uma opção válida!");
                    break;
            }
        }
        static void MenuEquipamentos()
        {
            Console.WriteLine("Selecione uma opção do Gerenciador de Equipamentos:");
            Console.WriteLine("  1 - Cadastrar Equipamento");
            Console.WriteLine("  2 - Listar Equipamentos");
            Console.WriteLine("  3 - Editar Equipamento");
            Console.WriteLine("  4 - Excluir Equipamento");
            Console.WriteLine("  5 - Voltar pro Menu Principal");

            int opcao;
            Console.Write("Opção: ");
            opcao = Convert.ToInt32(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    AdicionarEquipamento();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Listagem de Equipamentos: \n");
                    ListarEquipamentos();
                    break;
                case 3:
                    Console.Clear();
                    EditarEquipamento();
                    break;
                case 4:
                    Console.Clear();
                    ExcluirEquipamento();
                    break;
                case 5:
                    Console.Clear();
                    MenuPrincipal();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Informe uma das opções válidas!");
                    break;
            }
            MenuEquipamentos();
        }
        static void MenuChamados()
        {
            Console.WriteLine("Selecione uma opção do Gerenciador de Chamados:");
            Console.WriteLine("  1 - Abrir Chamado");
            Console.WriteLine("  2 - Listar Chamados");
            Console.WriteLine("  3 - Editar Chamado");
            Console.WriteLine("  4 - Excluir Chamado");
            Console.WriteLine("  5 - Voltar pro Menu Principal");

            int opcao;
            Console.Write("Opção: ");
            opcao = Convert.ToInt32(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    AbrirChamado();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("-- Lista de Chamados -- ");
                    ListarChamados();
                    break;
                case 3:
                    Console.Clear();
                    EditarChamado();
                    break;
                case 4:
                    Console.Clear();
                    ExcluirChamado();
                    break;
                case 5:
                    Console.Clear();
                    MenuPrincipal();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Informe uma das opções válidas!");
                    break;
            }
            MenuChamados();
        }
        #region Métodos de Equipamentos
        static void AdicionarEquipamento()
        {
            string nome;
            decimal precoAquisicao;
            string precoAquisicaoStr;
            string numeroSerie;
            string dataFabricacao;
            string fabricante;
            Console.WriteLine("Cadastro de Equipamento: \n");
            do
            {
                Console.WriteLine("  Informe o nome do equipamento: ");
                nome = Console.ReadLine();
                if (nome.Length < 6)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("    O nome do equipamento deve ter no mínimo 6 caracteres!");
                    Console.ResetColor();
                }
            } while (nome.Length < 6);

            Console.WriteLine("  Informe o preço de aquisição do equipamento (somente números): ");
            precoAquisicaoStr = Console.ReadLine().Replace(",", ".");
            Console.WriteLine("  Informe o número de serie do equipamento: ");
            numeroSerie = Console.ReadLine();
            Console.WriteLine("  Informe a data de fabricação do equipamento (dd/mm/aaaa): ");
            dataFabricacao = Console.ReadLine();
            Console.WriteLine("  Informe o nome do fabricante do equipamento: ");
            fabricante = Console.ReadLine();

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(precoAquisicaoStr) || string.IsNullOrEmpty(numeroSerie) || string.IsNullOrEmpty(dataFabricacao) || string.IsNullOrEmpty(fabricante))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Todos os campos são obrigatórios!");
                Console.ResetColor();
                Console.WriteLine("Voltando ao Menu de Equipamentos...\n");
                MenuEquipamentos();
            }

            precoAquisicao = Convert.ToDecimal(precoAquisicaoStr);

            DateTime data = DateTime.ParseExact(dataFabricacao, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            long dataFabricacaoUnix = ((DateTimeOffset)data).ToUnixTimeSeconds();

            Console.Clear();
            Console.WriteLine("Cadastrando equipamento...\n");
            int idEquipamento = 0;
            if (equipamentoNome.Count == 0)
            {
                idEquipamento = 1;
            }
            else
            {
                var ultimaChave = equipamentoNome.Keys.Last();
                idEquipamento = ultimaChave + 1;
            }
            equipamentoNome.Add(idEquipamento, nome);

            equipamentoPrecoAquisicao.Add(idEquipamento, precoAquisicao);
            equipamentoNumeroSerie.Add(idEquipamento, numeroSerie);
            equipamentoDataFabricacao.Add(idEquipamento, dataFabricacaoUnix);
            equipamentoFabricante.Add(idEquipamento, fabricante);

            Console.WriteLine("----------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Equipamento cadastrado com sucesso!");
            Console.ResetColor();
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("Voltando ao Menu de Equipamentos...\n");
            MenuEquipamentos();

        }
        static void ListarEquipamentos()
        {
            foreach (var chave in equipamentoNome.Keys)
            {
                Console.WriteLine("ID: " + chave);
                Console.WriteLine("Nome: " + equipamentoNome[chave]);
                Console.WriteLine("Preço de Aquisição: " + equipamentoPrecoAquisicao[chave]);
                Console.WriteLine("Número de Série: " + equipamentoNumeroSerie[chave]);
                Console.WriteLine("Data de Fabricação: " + equipamentoDataFabricacao[chave]);
                Console.WriteLine("Fabricante: " + equipamentoFabricante[chave]);
                Console.WriteLine("----------------------------------------\n");
            }
        }
        static void EditarEquipamento()
        {
            Console.WriteLine("Equipamentos disponíveis para edição: ");
            ListarEquipamentos();
            Console.WriteLine("Informe o ID do equipamento que deseja editar: ");
            int equipamento = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Informe o novo nome do equipamento: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Informe o novo preço de aquisição do equipamento: ");
            decimal precoAquisicao = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Informe o novo número de série do equipamento: ");
            string numeroSerie = Console.ReadLine();
            Console.WriteLine("Informe a nova data de fabricação do equipamento: ");
            string dataFabricacao = Console.ReadLine();
            Console.WriteLine("Informe o novo fabricante do equipamento: ");
            string fabricante = Console.ReadLine();
            if (equipamentoNome.Count < equipamento)
            {
                Console.WriteLine("Equipamento não encontrado!");
                Console.WriteLine("Voltando ao Menu de Equipamentos...\n");
                MenuEquipamentos();
            }
            DateTime data = DateTime.ParseExact(dataFabricacao, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            long dataFabricacaoUnix = ((DateTimeOffset)data).ToUnixTimeSeconds();


            Console.WriteLine("Editando equipamento...\n");
            if (nome.Length > 6)
            {
                equipamentoNome[equipamento] = nome;
            }
            else
            {
                Console.WriteLine("O nome do equipamento deve ter no mínimo 6 caracteres!");
                Console.WriteLine("O nome antigo será mantido!");
            }
            equipamentoPrecoAquisicao[equipamento] = precoAquisicao;
            equipamentoNumeroSerie[equipamento] = numeroSerie;
            equipamentoDataFabricacao[equipamento] = dataFabricacaoUnix;
            equipamentoFabricante[equipamento] = fabricante;
            Console.WriteLine("----------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Equipamento editado com sucesso!");
            Console.ResetColor();
            Console.WriteLine("----------------------------------------\n");
        }
        static void ExcluirEquipamento()
        {
            Console.WriteLine("Equipamentos disponíveis para exclusão: ");
            ListarEquipamentos();
            Console.WriteLine("Informe o ID do equipamento que deseja excluir: ");
            int equipamento = Convert.ToInt32(Console.ReadLine());
            if (equipamentoNome.Count < equipamento)
            {
                Console.WriteLine("Equipamento não encontrado!");
                Console.WriteLine("Voltando ao Menu de Equipamentos...\n");
                MenuEquipamentos();
            }

            Console.Clear();
            Console.WriteLine("Excluindo equipamento...\n");
            equipamentoNome.Remove(equipamento);
            equipamentoPrecoAquisicao.Remove(equipamento);
            equipamentoNumeroSerie.Remove(equipamento);
            equipamentoDataFabricacao.Remove(equipamento);
            equipamentoFabricante.Remove(equipamento);

            Console.WriteLine("----------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Equipamento excluído com sucesso!");
            Console.ResetColor();
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("Voltando ao Menu de Equipamentos...\n");
            MenuEquipamentos();
        }
        #endregion
        #region Menu de Chamados
        static void AbrirChamado()
        {
            Console.WriteLine("-- Abertura de Chamado -- ");
            Console.Write("  Informe o titulo do chamado: ");
            string titulo = Console.ReadLine();
            Console.Write("  Informe a descrição do chamado: ");
            string descricao = Console.ReadLine();
            Console.Write("  Informe a data de abertura do chamado (dd/mm/aaaa): ");
            string dataAbertura = Console.ReadLine();
            Console.WriteLine("  Equipamentos disponíveis para anexar ao chamado: ");
            ListarEquipamentos();
            int idEquipamento;
            do
            {
                Console.Write("  Informe o ID do equipamento a ser anexado ao chamado: ");
                if (!int.TryParse(Console.ReadLine(), out idEquipamento) || idEquipamento <= 0)
                {
                    Console.WriteLine(" O ID do equipamento deve ser um número inteiro maior que 0!");
                }
            } while (idEquipamento <= 0);

            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(descricao) || string.IsNullOrEmpty(dataAbertura) || idEquipamento <= 0)
            {
                Console.Clear();
                Console.WriteLine("Todos os campos são obrigatórios!");
                Console.WriteLine("Voltando ao Menu de Chamados...");
                MenuChamados();
            }

            DateTime data = DateTime.ParseExact(dataAbertura, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            long dataAberturaUnix = ((DateTimeOffset)data).ToUnixTimeSeconds();
            Console.WriteLine("Criando chamado...");
            int idChamado = 0;
            if (chamadoTitulo.Count == 0)
            {
                idChamado = 1;
            }
            else
            {
                var ultimaChave = chamadoTitulo.Keys.Last();
                idChamado = ultimaChave + 1;
            }
            chamadoTitulo.Add(idChamado, titulo);
            chamadoDescricao.Add(idChamado, descricao);
            chamadoDataAbertura.Add(idChamado, dataAberturaUnix);
            chamadoEquipamento.Add(idChamado, idEquipamento);
            Console.WriteLine("----------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Chamado criado com sucesso!");
            Console.ResetColor();
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("Voltando ao Menu de Chamados...\n");
        }
        static void ListarChamados()
        {
            if (chamadoTitulo.Count == 0)
            {
                Console.WriteLine("Nenhum chamado encontrado!");
            }
            foreach (var chave in chamadoTitulo.Keys)
            {
                Console.WriteLine("ID: " + chave);
                Console.WriteLine("Título: " + chamadoTitulo[chave]);
                Console.WriteLine("Descrição: " + chamadoDescricao[chave]);
                Console.WriteLine("Data de Abertura: " + chamadoDataAbertura[chave]);
                Console.WriteLine("Equipamento: " + chamadoEquipamento[chave]);
                Console.WriteLine("----------------------------------------\n");
            }
        }
        static void EditarChamado()
        {
            Console.WriteLine("-- Edição de Chamado -- ");
            Console.WriteLine("Chamados disponíveis para edição: ");
            ListarChamados();
            Console.WriteLine("Informe o ID do chamado que deseja editar: ");
            int chamado = Convert.ToInt32(Console.ReadLine());
            if (chamadoTitulo.Count < chamado)
            {
                Console.WriteLine("Chamado não encontrado!");
                Console.WriteLine("Voltando ao Menu de Chamados...\n");
                MenuChamados();
            }
            Console.WriteLine("Informe o novo título do chamado: ");
            string titulo = Console.ReadLine();
            Console.WriteLine("Informe a nova descrição do chamado: ");
            string descricao = Console.ReadLine();
            Console.WriteLine("Informe a nova data de abertura do chamado: ");
            string dataAbertura = Console.ReadLine();
            ListarEquipamentos();
            Console.WriteLine("Informe o novo ID do equipamento a ser anexado ao chamado: ");
            int equipamento = Convert.ToInt32(Console.ReadLine());
            if (equipamento > equipamentoNome.Count )
            {
                Console.WriteLine("Equipamento não encontrado!");
                Console.WriteLine("Voltando ao Menu de Chamados...\n");
                MenuChamados();
            }
            DateTime data = DateTime.ParseExact(dataAbertura, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            long dataAberturaUnix = ((DateTimeOffset)data).ToUnixTimeSeconds();

            Console.Clear();
            Console.WriteLine("Editando chamado...");
            chamadoTitulo[chamado] = titulo;
            chamadoDescricao[chamado] = descricao;
            chamadoDataAbertura[chamado] = dataAberturaUnix;
            chamadoEquipamento[chamado] = equipamento;
            Console.WriteLine("----------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Chamado editado com sucesso!");
            Console.ResetColor();
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("Voltando ao Menu de Chamados...\n");
        }
        static void ExcluirChamado()
        {
            Console.WriteLine("-- Exclusão de Chamado -- ");
            Console.WriteLine("Chamados disponíveis para exclusão: ");
            ListarChamados();
            Console.WriteLine("Informe o ID do chamado que deseja excluir: ");
            int chamado = Convert.ToInt32(Console.ReadLine());
            if (chamadoTitulo.Count < chamado)
            {
                Console.WriteLine("Chamado não encontrado!");
                Console.WriteLine("Voltando ao Menu de Chamados...\n");
                MenuChamados();
            }
            Console.Clear();
            Console.WriteLine("Excluindo chamado...\n");
            chamadoTitulo.Remove(chamado);
            chamadoDescricao.Remove(chamado);
            chamadoDataAbertura.Remove(chamado);
            chamadoEquipamento.Remove(chamado);
            Console.WriteLine("----------------------------------------\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Chamado excluído com sucesso!");
            Console.ResetColor();
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("Voltando ao Menu de Chamados...\n");
            MenuChamados();
        }
        #endregion
    }
}