namespace ComponentesVisuais01
{
    public partial class FormMain : Form
    {
        List<Estado> estados = new List<Estado>();
        public FormMain()
        {
            InitializeComponent();
            PopularEstados();
            InicializarCampos();
        }

        /*
        private void mnSalvar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("salvando arquivo....", "Informação",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        */
        private void InicializarCampos()
        {
            cbxEstado.DisplayMember = "Nome";
            cbxEstado.ValueMember = "Sigla";
            cbxEstado.DataSource = estados;
        }

        public void PopularEstados()
        {
            estados.Add(new Estado("ES", "Espírito Santo"));
            estados.Add(new Estado("MG", "Mina Gerais"));
            estados.Add(new Estado("RJ", "Rio de Janeiro"));
            estados.Add(new Estado("SP", "São Paulo"));
            estados.Add(new Estado("PR", "Paraná"));
            estados.Add(new Estado("RS", "Rio Grande do Sul"));
            estados.Add(new Estado("SC", "Santa Catarina"));
            estados.Add(new Estado("MT", "Mato Grosso"));
            estados.Add(new Estado("PE", "Pernambuco"));
            estados.Add(new Estado("AM", "Amazonas"));
        }




    }
}