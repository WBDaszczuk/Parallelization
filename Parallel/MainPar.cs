using System.Numerics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Parallel
{
    public partial class MainPar : Form
    {
        public MainPar()
        {
            InitializeComponent();
        }

        private static string[] merge(string[] v1, string[] v2)
        {
            string[] v = new string[v1.Length + v2.Length];
            int i = 0, i1 = 0, i2 = 0;
            while (i1 < v1.Length && i2 < v2.Length)
            {
                if (v1[i1] == "") { v[i] = v2[i2]; i2++; }
                else if (String.Compare(v1[i1], v2[i2]) < 0 || v2[i2] == "") { v[i] = v1[i1]; i1++; }
                else { v[i] = v2[i2]; i2++; }
                i++;
            }
            while (i1 < v1.Length) { v[i] = v1[i1]; i1++; i++; }
            while (i2 < v2.Length) { v[i] = v2[i2]; i2++; i++; }
            return v;
        }

        private static string[] mergeSort(string[] v)
        {
            if (v.Length <= 1) return v;
            int middle = v.Length / 2;
            string[] v1 = v.Skip(0)
                    .Take(middle)
                    .ToArray();
            v1 = mergeSort(v1);
            string[] v2 = v.Skip(middle)
                .Take(v.Length - middle)
                .ToArray();
            v2 = mergeSort(v2);
            return merge(v1, v2);
        }

        static string[] v1s;
        private static void mergeSortThread()
        {
            v1s = mergeSort(v1s);
        }

        private static string[] mergeSortParallel(string[] v)
        {
            if (v.Length <= 1) return v;
            int middle = v.Length / 2;
            v1s = v.Skip(0)
                    .Take(middle)
                    .ToArray();
            Thread thread = new Thread(new ThreadStart(mergeSortThread));
            thread.Start();
            string[] v2 = v.Skip(middle)
                .Take(v.Length - middle)
                .ToArray();
            v2 = mergeSort(v2);
            thread.Join();
            return merge(v1s, v2);
        }

        static string[][] vg = new string[4][];
        private static void mergeSortMemberGang(object i)
        {
            vg[(int)i] = mergeSort(vg[(int)i]);
        }

        private static string[] mergeSortGang(string[] v)
        {
            if (v.Length <= 1) return v;
            if (v.Length <= 3) return mergeSortParallel(v);
            int part = v.Length / 4;
            Thread[] threads = new Thread[4];
            object[] obj = new object[4] { 0, 1, 2, 3 };
            vg[0] = v.Skip(0)
                    .Take(part)
                    .ToArray();
            threads[0] = new Thread(new ParameterizedThreadStart(mergeSortMemberGang));
            threads[0].Start(obj[0]);
            vg[1] = v.Skip(part)
                .Take(part)
                .ToArray();
            threads[1] = new Thread(new ParameterizedThreadStart(mergeSortMemberGang));
            threads[1].Start(obj[1]);
            vg[2] = v.Skip(part * 2)
                .Take(part)
                .ToArray();
            threads[2] = new Thread(new ParameterizedThreadStart(mergeSortMemberGang));
            threads[2].Start(obj[2]);
            vg[3] = v.Skip(part * 3)
                .Take(v.Length - 3 * part)
                .ToArray();
            threads[3] = new Thread(new ParameterizedThreadStart(mergeSortMemberGang));
            threads[3].Start(obj[3]);
            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();
            vg[0] = merge(vg[0], vg[1]);
            vg[2] = merge(vg[2], vg[3]);
            return merge(vg[0], vg[2]);
        }


        static string[][] vf = new string[4][];
        private static void mergeSortMemberFor(object i)
        {
            vf[(int)i] = mergeSort(vf[(int)i]);
        }


        private static string[] mergeSortFor(string[] v)
        {
            if (v.Length <= 1) return v;
            if (v.Length <= 3) return mergeSortParallel(v);
            int part = v.Length / 4;
            Thread[] threads = new Thread[4];
            object[] obj = new object[4];
            for (int i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(new ParameterizedThreadStart(mergeSortMemberFor));
                obj[i] = i;
                if (i < 3) vf[i] = v.Skip(i * part).Take(part).ToArray();
                else vf[3] = v.Skip(3 * part).Take(v.Length - 3 * part).ToArray();
                threads[i].Start(obj[i]);
            }
            for (int i = 0; i < threads.Length; ++i)
                threads[i].Join();
            vf[0] = merge(vf[0], vf[1]);
            vf[2] = merge(vf[2], vf[3]);
            return merge(vf[0], vf[2]);
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            sortButton.BackColor = Color.LightGray;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DateTime start = DateTime.Now;
                    string name = openFileDialog.FileName;
                    //StringReader reader = new StringReader(name);
                    //string[] words = reader.ReadToEnd().Split(' ', '\t', '\n');

                    var file = File.ReadAllText(name, Encoding.ASCII).Split(new[] { ' ', '\t', '\n', '\r' });
                    string[] sorted = mergeSort(file);
                    sortButton.BackColor = Color.Red;
                    sortTimeLabel.Text = "time: " + (DateTime.Now - start).Duration().ToString();
                }
            }
        }

        private void parSortButton_Click(object sender, EventArgs e)
        {
            parSortButton.BackColor = Color.LightGray;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DateTime start = DateTime.Now;
                    string name = openFileDialog.FileName;
                    //StringReader reader = new StringReader(name);
                    //string[] words = reader.ReadToEnd().Split(' ', '\t', '\n');

                    var file = File.ReadAllText(name, Encoding.ASCII).Split(new[] { ' ', '\t', '\n', '\r' });
                    string[] sorted = mergeSortParallel(file);
                    parSortButton.BackColor = Color.Red;
                    parTimeLabel.Text = "time: " + (DateTime.Now - start).Duration().ToString();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gangSortButton_Click(object sender, EventArgs e)
        {
            gangSortButton.BackColor = Color.LightGray;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DateTime start = DateTime.Now;
                    string name = openFileDialog.FileName;
                    //StringReader reader = new StringReader(name);
                    //string[] words = reader.ReadToEnd().Split(' ', '\t', '\n');

                    var file = File.ReadAllText(name, Encoding.ASCII).Split(new[] { ' ', '\t', '\n', '\r' });
                    string[] sorted = mergeSortGang(file);
                    gangSortButton.BackColor = Color.Red;
                    gangTimeLabel.Text = "time: " + (DateTime.Now - start).Duration().ToString();
                }
            }
        }

        private void forSortButton_Click(object sender, EventArgs e)
        {
            forSortButton.BackColor = Color.LightGray;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DateTime start = DateTime.Now;
                    string name = openFileDialog.FileName;
                    //StringReader reader = new StringReader(name);
                    //string[] words = reader.ReadToEnd().Split(' ', '\t', '\n');

                    var file = File.ReadAllText(name, Encoding.ASCII).Split(new[] { ' ', '\t', '\n', '\r' });
                    string[] sorted = mergeSortFor(file);
                    forSortButton.BackColor = Color.Red;
                    forTimeLabel.Text = "time: " + (DateTime.Now - start).Duration().ToString();
                }
            }
        }
    }
}