using BusnLogicBW;
using DALMemoryStore;

namespace OpdrachtDALBjörn
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BrommerContainer bc = new BrommerContainer(new BrommerMSDAL());

            bc.Create(new Brommer(1, "Audi", "DBB-11-B", 5000));
            bc.Create(new Brommer(2, "BMW", "DTTB-11-B", 5000));
            bc.Create(new Brommer(3, "BMW", "DCB-11-B", 6000));
            Brommer b8 = new Brommer(4, "BMW", "ABCD-11-B", 8000);
            bc.Create(b8);
            Brommer henk = bc.FindByID(2);
            bc.Delete(henk);
            List<Brommer> lijst = bc.FindByMerk("BMW");

            Brommer b = bc.FindByID(1);
            b.Kenteken = "BOI-JE-6";
            bc.Update(b);
            Brommer b2 = bc.FindByKenteken("BOI-JE-6");
            MessageBox.Show(lijst.Count.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}