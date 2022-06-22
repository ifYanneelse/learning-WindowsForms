namespace CadastroDeClientes
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            AtualizarComboBoxPaises();
            CriarControlesEstadosCivis();
            DesabilitarCampos();
        }

        //exibição de uma informação
        private void Informar(string mensagem)
        {
            MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        //exibição de uma pergunta
        private bool Confirmar(string pergunta)
        {
            return MessageBox.Show(pergunta, "Confirmação", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void AtualizarComboBoxPaises()
        {//vai exibir o nome dos países no combox de modo escrito. Para ser ordem alfabética, ir no design "sorted=true"
            cbxNacionalidade.DataSource = Pais.Listagem;//se conecta ao objeto Pais
            cbxNacionalidade.DisplayMember = ""; //inicia vazio
            cbxNacionalidade.DisplayMember = "Nome"; //vai ser exibido o nome do país
            cbxNacionalidade.ValueMember = "Sigla"; //quando pegar o valor vai passar a sigla
            cbxNacionalidade.SelectedIndex = -1; //começa sem uma país selecionado
        }


        //metodo que atualiza o combobox de clientes
        private void AtualizarComoBoxClientes()
        {
            cbxCliente.DataSource = Cliente.Listagem; //recebem a lista que está na classee Cliente
            cbxCliente.DisplayMember = ""; 
            cbxCliente.DisplayMember = "Nome"; 
            cbxCliente.ValueMember = "Codigo";//vai receber o codigo, mas só vai mostrar o nome
        }


        private void CorrigirTabStop(object sender, EventArgs e)
        {
            ((RadioButton)sender).TabStop = true;
        }//se ficar falso alguma tabulação, passar pra 'true'


        //metodo que pega os estados civis e cria radio buttons a partir dos enumerados 
        private void CriarControlesEstadosCivis()
        {
            int iRB = 0; //inicia o indice com 0
            var estadosCivis = Enum.GetValues(typeof(EnumEstadoCivil)); //pega os valores do tipo enum estados civis
            foreach (var ec in estadosCivis) //pra cada valor no vetor criar um novo radio button, chamado de rb:
            {
                RadioButton rb = new RadioButton()
                {
                    Text = ec.ToString(), //texto do radio button
                    Location = new Point(10, (iRB + 1) * 27), //local na tela, um embaixo do outro
                    Width = 85,
                    TabStop = true,
                    TabIndex = iRB,
                    Tag = ec //tag recebe o estado civil || recebe o valor do estado civil não o texto
                };

                rb.TabStopChanged += new EventHandler(CorrigirTabStop); //pra corrigir um problema de tabulação e passar por todo componentes- chama o outro método 'CorrigirTabStop'
                gbxEstadoCivil.Controls.Add(rb); //depois de tudo, agrupa ele e adiciona um por um
                iRB++;
            }
        }

        //metodo que le o estado civil selecionado pelo usuario
        //qual radio button está selecionado?
        private EnumEstadoCivil  ? LerEstadoCivil() //metodo que retorn estado civil | ? = pode ser nulo, o usuario pode não ter selecionando ainda | vai ler o estado civil ou nulo
        {
            foreach (var control in gbxEstadoCivil.Controls)//pra cada controle, dentro da coleção de controles do 'group box'
            {
                //conversão do objeto genérico 'control' pra radio button
                RadioButton rb = control as RadioButton;
                if (rb.Checked) //se ele está marcado
                {
                    return (EnumEstadoCivil)(rb.Tag); //pega a tag onde está guardado o valor do estado civil
                }                                       // depois ele converte pra estado civil e retorna o valor dela
            }
            return null; // retorna nulo, se não foi marcado nada e não veio nenhum valor
        }

        //agora vai marcar o combobox a partir de um estado civil que for passado
        private void MarcarEstadoCivil(EnumEstadoCivil estadoCivil)
        {
            foreach (var control in gbxEstadoCivil.Controls)
            {
                RadioButton rb = control as RadioButton;
                if ((EnumEstadoCivil)(rb.Tag) == estadoCivil) //depois de percorrer vai verificar se a opção marca é a mesma recebida pelo parametro
                {
                    rb.Checked = true; //se for igual, o checked vai ficar como true
                    return; //encontrado o checked, sai do metodo sem precisar checar tudo de novo
                }
            }
        }

        //metodo pra limpar os campos
        private void LimparCampos()
        {
            txtCodigo.Clear(); //metodo pronto igual a txtCodigo.Text = ""
            txtNome.Clear();
            mtbCPF.Clear();
            dtpDataNascimento.Value = DateTime.Now.Date;
            nudRendaMensal.Value = 0;
            foreach (var control in gbxEstadoCivil.Controls)
            {
                (control as RadioButton).Checked = false;
            }
            cbxNacionalidade.SelectedIndex = -1;
            mtbPlacaVeiculo.Clear();
            chkTemFilhos.CheckState = CheckState.Indeterminate;

        }

        //metodo que mostra os dados do cliente que for selecionado no formulario
        private void PreencherCamposComCliente (Cliente cliente)
        {
            txtCodigo.Text = cliente.Cod.ToString();
            txtNome.Text = cliente.Nome;
            mtbCPF.Text = cliente.CPF.ToString();
            dtpDataNascimento.Value = cliente.DataNascimento;
            nudRendaMensal.Value = cliente.RendaMensal;
            MarcarEstadoCivil(cliente.EstadoCivil);
            cbxNacionalidade.SelectedValue = cliente.Nacionalidade;
            mtbPlacaVeiculo.Text = cliente.PlacaVeiculo;
            chkTemFilhos.Checked = cliente.TemFilhos;
        }

        //salvar os dados que estão nos campos em um objeto do tipo cliente
        private void PreencherClienteComCampos(Cliente cliente)
        {
            cliente.Nome = txtNome.Text;
            cliente.CPF = Convert.ToInt64(mtbCPF.Text); //pra caber o cpf em numerico
            cliente.DataNascimento = dtpDataNascimento.Value.Date;
            cliente.RendaMensal = nudRendaMensal.Value;
            cliente.EstadoCivil = LerEstadoCivil().Value;
            cliente.TemFilhos = chkTemFilhos.Checked;
            cliente.Nacionalidade = cbxNacionalidade.SelectedValue.ToString();
            cliente.PlacaVeiculo = mtbPlacaVeiculo.Text;
        }

        //verificar se algum campo ficou em branco
        private bool PreencheuTodosOsCampos()
        {
            if (String.IsNullOrWhiteSpace(txtNome.Text)) return false;
            if (String.IsNullOrWhiteSpace(mtbCPF.Text)) return false;
            if (dtpDataNascimento.Value.Date == DateTime.Now.Date) return false;
            if (nudRendaMensal.Value == 0) return false;
            if (LerEstadoCivil() == null) return false;
            if (cbxNacionalidade.SelectedIndex < 0) return false;
            if (String.IsNullOrEmpty(mtbPlacaVeiculo.Text)) return false;
            if (chkTemFilhos.CheckState == CheckState.Indeterminate) return false;

            return true;
        }

        //se existe cliente
        private bool PossuiValoresNaoSalvos()
        {
            if (cbxCliente.SelectedIndex < 0) //checa primeiro o indice do cliente, se for menor que 0, nenhum cliente está selecionado | ou seja é um novo cliente
            {
                if (!String.IsNullOrWhiteSpace(txtNome.Text)) return true;
                if (!String.IsNullOrWhiteSpace(mtbCPF.Text)) return true;
                if (dtpDataNascimento.Value.Date != DateTime.Now.Date) return true;
                if (nudRendaMensal.Value > 0) return true;
                if (LerEstadoCivil() != null) return true;
                if (cbxNacionalidade.SelectedIndex >= 0) return true;
                if (!String.IsNullOrEmpty(mtbPlacaVeiculo.Text)) return true;
                if (chkTemFilhos.CheckState != CheckState.Indeterminate) return true;
            }
            else //quando existe o cliente
            {
                Cliente cliente = cbxCliente.SelectedItem as Cliente; //captura o cliente q está selecionado no combox, guarda na variavel cliente e compara os valores se tem algum diferente
                if (txtNome.Text.Trim() != cliente.Nome) return true;
                if (mtbCPF.Text != cliente.CPF.ToString()) return true;
                if (dtpDataNascimento.Value.Date != cliente.DataNascimento) return true;
                if (nudRendaMensal.Value != cliente.RendaMensal) return true;
                if (LerEstadoCivil() != cliente.EstadoCivil) return true;
                if (cbxNacionalidade.SelectedValue.ToString() != cliente.Nacionalidade) return true;
                if (mtbPlacaVeiculo.Text != cliente.PlacaVeiculo) return true;
                if (chkTemFilhos.Checked != cliente.TemFilhos) return true;
            }
            return false;

        }

        //troca o status do campus : habilitado ou desabilitado
        private void AlterarEstadoDosCampos(bool estado)
        {
            txtNome.Enabled = estado;
            mtbCPF.Enabled = estado;
            dtpDataNascimento.Enabled = estado;
            nudRendaMensal.Enabled = estado;
            gbxEstadoCivil.Enabled = estado;
            cbxNacionalidade.Enabled = estado;
            mtbPlacaVeiculo.Enabled = estado;   
            chkTemFilhos.Enabled = estado;
            btnSalvar.Enabled = estado;
            btnCancelar.Enabled = estado;
        }

        private void HabilitarCampos()
        {
            AlterarEstadoDosCampos(true);
        }

        private void DesabilitarCampos()
        {
            AlterarEstadoDosCampos(false);
        }

        //botao novo-add cliente
        private void btnNovo_Click(object sender, EventArgs e)
        {
            cbxCliente.SelectedIndex = -1; //começa sem nenhum cliente
            LimparCampos();
            HabilitarCampos();
            txtNome.Select(); //começa e foca no campo nome
        }

        //botao salvar
        private void btnSalvar_Click(object sender, EventArgs e)
        {//verifica se todos os campos foram preenchidos:
            if (PreencheuTodosOsCampos()) 
            {
                Cliente cliente = cbxCliente.SelectedIndex < 0 ? //se for uma inclusão, vai ser menor que 0, então
                    new Cliente() : cbxCliente.SelectedItem as Cliente; //cria um novo cliente | se não for menor q 0, existe uma alteração, do cliente que está no combobox
                
                //todos os dados do campus será passado pra variavel cliente
                PreencherClienteComCampos(cliente);  //esse metodo bota os valores do cliente selecionado
                DesabilitarCampos();

                if (cbxCliente.SelectedIndex < 0) //se for inserção de um novo cliente:
                {
                    cliente = Cliente.Inserir(cliente); //bota na lista de clientes
                    AtualizarComoBoxClientes();
                    Informar("Cliente cadastrado com sucesso!");
                }
                else // se for uma alteração:
                {
                    AtualizarComoBoxClientes();
                    Informar("Cliente alterado com sucesso!");
                }

            }
            else //se o usuario não preencheu todos os campos
            {
                Informar("Só é possivel salvar se todos os campos forem preenchidos." +
                    "Salvamento não realizado");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (PossuiValoresNaoSalvos())
            {
                if (Confirmar("Há alterações não salvas. Deseja realmente cancelar?"))
                {
                    if (cbxCliente.SelectedIndex == -1) //se for botar um novo cliente e clicar em cancelar = limpa os campos
                        LimparCampos();
                    else //se não, restaura oa dados que tinha antes
                        PreencherCamposComCliente(cbxCliente.SelectedItem as Cliente);
                    cbxCliente.Select();
                    DesabilitarCampos();
                }
            }
            else //caso não possua valores não salvos:
            {
                if (cbxCliente.SelectedIndex == -1) //se for botar um novo cliente e clicar em cancelar = limpa os campos
                    LimparCampos();
                else //se não, restaura oa dados que tinha antes
                    PreencherCamposComCliente(cbxCliente.SelectedItem as Cliente);
                cbxCliente.Select();
                DesabilitarCampos();
               
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        //evento acontece antes do botão fechar
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PossuiValoresNaoSalvos())
                e.Cancel = !Confirmar("Há alterações não salvar. Deseja realmente sair?");
            else
                e.Cancel = !Confirmar("Deseja realmente sair?");
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            HabilitarCampos();
            PreencherCamposComCliente(cbxCliente.SelectedItem as Cliente); //quando clicar no cliente, retornar com os dados que foi gravado
            txtNome.Select();
        }

        //quando o cliente for habilitado, o botão alterar precisa mudar
        private void cbxCliente_SelectedIndexChanged(object sender, EventArgs e) //o indice do item selecionado foi alterado
        {
            if (cbxCliente.SelectedIndex < 0) //caso esse indice for menor que 0 = não tem cliente selecionado
            {
                btnAlterar.Enabled = false; // o botão alterar ficará desabilitado
            }
            else
            {
                PreencherCamposComCliente(cbxCliente.SelectedItem as Cliente);
                btnAlterar.Enabled = true;
            }
        }
    }
}