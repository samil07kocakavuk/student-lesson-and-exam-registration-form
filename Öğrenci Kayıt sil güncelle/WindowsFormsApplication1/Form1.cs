using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=marketplace1.accdb");
        private void veri()
        {
            string SQL = "select * from ogrencı";
            bool kont = false;
            if (textBox1.Text != "" || textBox2.Text != "" || comboBox1.Text != "" || comboBox2.Text != "" || comboBox3.Text != "")
            {  
                SQL += " where ";
            }
            if (textBox1.Text != "")
            {
                kont = true;
                SQL += " ogrno  = '" + textBox1.Text + "'";
            }
            if (textBox2.Text != "")
            {
                if (!kont)
                {
                    kont = true;
                }
                else
                {
                    SQL += "and";
                }
                SQL += " ograd  = '" + textBox2.Text + "'";
            }
            if (comboBox1.Text != "")
            {
                if (!kont)
                {
                    kont = true;
                }
                else
                {
                    SQL += " and";
                }
                SQL += " ogrsınıf  = '" + comboBox1.Text + "'";
            }
            if (comboBox2.Text  != "")
            {
                if (!kont)
                {
                    kont = true;
                }
                else
                {
                    SQL += " and";
                }
                SQL += " ogrsub  = '" + comboBox2.Text + "'";
            }
            if (comboBox3.Text != "")
            {
                if (!kont)
                {
                    kont = true;
                }
                else
                {
                    SQL += " and";
                }
                SQL += " ogrbol = '" + comboBox3.Text + "';";
            }
            bag.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bag;
            komut.CommandText = SQL;
            MessageBox.Show(SQL);
            OleDbDataReader oku = komut.ExecuteReader();
            listView1.Items.Clear();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["ogrno"].ToString();
                ekle.SubItems.Add(oku["ograd"].ToString());
                ekle.SubItems.Add(oku["ogrsınıf"].ToString());
                ekle.SubItems.Add(oku["ogrsub"].ToString());
                ekle.SubItems.Add(oku["ogrbol"].ToString());
                listView1.Items.Add(ekle);
            }
            bag.Close();
            label6.Text = komut.CommandText.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Kaydetmek İstıyonmu ??? ", "Uyarı Mesajı", MessageBoxButtons.YesNoCancel);
            if (cevap == DialogResult.Yes)
            {
                bag.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = bag;
                komut.CommandText = "insert into ogrencı (ogrno,ograd,ogrsınıf,ogrsub,ogrbol)values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "')";
                komut.ExecuteNonQuery();
                bag.Close();
                veri();
            }
            else
            {
                MessageBox.Show("İptal edildi");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bag.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bag;
            komut.CommandText = "select * from ogrencı where ogrno = '" + textBox1.Text + "'";
            OleDbDataReader oku = komut.ExecuteReader();
            oku.Read();
            textBox2.Text = oku["ograd"].ToString();
            comboBox1.Text = oku["ogrsınıf"].ToString();
            comboBox2.Text = oku["ogrsub"].ToString();
            comboBox3.Text = oku["ogrbol"].ToString();
            bag.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Silmek İstıyonmu ??? ", "Uyarı Mesajı", MessageBoxButtons.YesNoCancel);
            if (cevap == DialogResult.Yes)
            {
                bag.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = bag;
                komut.CommandText = "delete from ogrencı where ogrsub = '" + comboBox2.Text + "'";
                komut.ExecuteNonQuery();
                bag.Close();
                veri();
            }
            else
            {
                MessageBox.Show("İptal edildi");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Güncellemek İstıyonmu ??? ", "Uyarı Mesajı", MessageBoxButtons.YesNoCancel);
            if (cevap == DialogResult.Yes)
            {
                bag.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = bag;
                komut.CommandText = "update ogrencı set ogrno = '" + textBox1.Text.ToString() + "', ograd ='" + textBox2.Text.ToString() + "', ogrsınıf = '" + comboBox1.Text + "',ogrsub= '" + comboBox2.Text + "',ogrbol = '" + comboBox3.Text + "' where ogrno = '" + textBox1.Text + "'";
                komut.ExecuteNonQuery();
                bag.Close();
                veri();
            }
            else
            {
                MessageBox.Show("İptal edildi");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            veri();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            comboBox1.Text = listView1.SelectedItems[0].SubItems[2].Text;
            comboBox2.Text = listView1.SelectedItems[0].SubItems[3].Text;
            comboBox3.Text = listView1.SelectedItems[0].SubItems[4].Text;
            label7.Text = listView1.SelectedItems.Count.ToString();
        }
        /// <summary>
        /// ////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        /// ////////////////////////////Öğrenci Kayıt Bitişş\\\\\\\\\\\\\\\\\\
        /// ////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        private void ders_veri() {
            string SQL = "select * from dersler";
            bool kont = false;
            if (textBox3.Text != "" || textBox4.Text != "" || comboBox4.Text != "" || comboBox5.Text != "" || comboBox6.Text != "")
            {
                SQL += " where ";
            }
            if (textBox3.Text != "")
            {
                kont = true;
                SQL += " derskod  = '" + textBox3.Text + "'";
            }
            if (textBox4.Text != "")
            {
                if (!kont)
                {
                    kont = true;
                }
                else
                {
                    SQL += "and";
                }
                SQL += " dersad  = '" + textBox4.Text + "'";
            }
            if (comboBox4.Text != "")
            {
                if (!kont)
                {
                    kont = true;
                }
                else
                {
                    SQL += " and";
                }
                SQL += " derssin  = '" + comboBox4.Text + "'";
            }
            if (comboBox5.Text != "")
            {
                if (!kont)
                {
                    kont = true;
                }
                else
                {
                    SQL += " and";
                }
                SQL += " derssaat  = "+ comboBox5.Text + "";
            }
            if (comboBox6.Text != "")
            {
                if (!kont)
                {
                    kont = true;
                }
                else
                {
                    SQL += " and";
                }
                SQL += " dersalan = '" + comboBox6.Text + "';";

            }
            bag.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bag;
            komut.CommandText = SQL;
            MessageBox.Show(SQL);
            OleDbDataReader oku = komut.ExecuteReader();
            listView2.Items.Clear();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["derskod"].ToString();
                ekle.SubItems.Add(oku["dersad"].ToString());
                ekle.SubItems.Add(oku["derssin"].ToString());
                ekle.SubItems.Add(oku["derssaat"].ToString());
                ekle.SubItems.Add(oku["dersalan"].ToString());
                listView2.Items.Add(ekle);
            }
            bag.Close();
            label6.Text = komut.CommandText.ToString();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Kaydetmek İstıyonmu ??? ", "Uyarı Mesajı", MessageBoxButtons.YesNoCancel);
            if (cevap == DialogResult.Yes)
            {
                bag.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = bag;
                komut.CommandText = "insert into dersler (derskod,dersad,derssin,derssaat,dersalan)values('" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox4.Text + "'," + comboBox5.Text + ",'" + comboBox6.Text + "')";
                komut.ExecuteNonQuery();
                bag.Close();
                ders_veri();
            }
            else
            {
                MessageBox.Show("İptal edildi");
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Silmek İstıyonmu ??? ", "Uyarı Mesajı", MessageBoxButtons.YesNoCancel);
            if (cevap == DialogResult.Yes)
            {
                bag.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = bag;
                komut.CommandText = "delete from dersler where derskod = '" + textBox3.Text + "'";
                komut.ExecuteNonQuery();
                bag.Close();
                ders_veri();
            }
            else
            {
                MessageBox.Show("İptal edildi");
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Güncellemek İstıyonmu ??? ", "Uyarı Mesajı", MessageBoxButtons.YesNoCancel);
            if (cevap == DialogResult.Yes)
            {
                bag.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = bag;
                komut.CommandText = "update dersler set derskod = '" + textBox3.Text.ToString() + "', dersad ='" + textBox4.Text.ToString() + "', derssin = '" + comboBox4.Text + "',derssaat= " + comboBox5.Text + ",dersalan = '" + comboBox6.Text + "' where derskod = '" + textBox3.Text + "'";
                komut.ExecuteNonQuery();
                bag.Close();
                ders_veri();
            }
            else
            {
                MessageBox.Show("İptal edildi");
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            ders_veri();
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
            {
                return;
            }
            textBox3.Text = listView2.SelectedItems[0].SubItems[0].Text;
            textBox4.Text = listView2.SelectedItems[0].SubItems[1].Text;
            comboBox4.Text = listView2.SelectedItems[0].SubItems[2].Text;
            comboBox5.Text = listView2.SelectedItems[0].SubItems[3].Text;
            comboBox6.Text = listView2.SelectedItems[0].SubItems[4].Text;
            label7.Text = listView2.SelectedItems.Count.ToString();
        }
        /// Ders Kısmı bitiş
        private void not_veri() {
            string SQL = "select * from ogrencı";
            bool kont = false;
            if (comboBox8.Text != "" || comboBox7.Text != "")
            {
                SQL += " where ";
            }
            if (comboBox8.Text != "")
            {
                kont = true;
                SQL += " ogrsınıf  = '" + comboBox8.Text + "'";
            }
            if (comboBox7.Text != "")
            {
                if (!kont)
                {
                    kont = true;
                }
                else
                {
                    SQL += "and";
                }
                SQL += " ogrsub  = '" + comboBox7.Text + "'";
            }
            bag.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bag;
            komut.CommandText = SQL;
            MessageBox.Show(SQL);
            OleDbDataReader oku = komut.ExecuteReader();
            listView3.Items.Clear();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["ogrno"].ToString();
                ekle.SubItems.Add(oku["ograd"].ToString());
                ekle.SubItems.Add(oku["ogrsınıf"].ToString());
                ekle.SubItems.Add(oku["ogrsub"].ToString());
                listView3.Items.Add(ekle);
            }
            bag.Close();
            label6.Text = komut.CommandText.ToString();
        }
        private void ders_combo(string sinif)
        {
            string SQL = "select * from dersler where derssin = '"+sinif+"'";
            bag.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bag;
            komut.CommandText = SQL;
            OleDbDataReader oku = komut.ExecuteReader();
            comboBox9.Items.Clear();
            while (oku.Read())
            {
                comboBox9.Items.Add(oku["dersad"].ToString());
            }
            bag.Close();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            not_veri();
        }
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count == 0)
            {
                return;
            }
            textBox5.Text = listView3.SelectedItems[0].SubItems[0].Text;
            textBox6.Text = listView3.SelectedItems[0].SubItems[1].Text;
            comboBox8.Text = listView3.SelectedItems[0].SubItems[2].Text;
            comboBox7.Text = listView3.SelectedItems[0].SubItems[3].Text;
            ders_combo(comboBox8.Text);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
        }
        private void button16_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
        }
        private void button15_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
        }
        
        private string derskodbul(string ad,string sinif)
        {
            string SQL = "select * from dersler where dersad = '" + ad + "'and derssin = '"+sinif+"'";
            bag.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bag;
            komut.CommandText = SQL;
            OleDbDataReader oku = komut.ExecuteReader();
            oku.Read();
            string a = oku["derskod"].ToString();
            bag.Close();
            return a;  
        }
        private string ogrsınıfbul(string numara)
        {
            string SQL = "select * from ogrencı where ogrno = '" + numara +"'";
            bag.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bag;
            komut.CommandText = SQL;
            OleDbDataReader oku = komut.ExecuteReader();
            oku.Read();
            string a = oku["ogrsınıf"].ToString();
            bag.Close();
            return a;             
        }  
        private void button10_Click(object sender, EventArgs e)
        {
            string derskod;
            string sınıf;
            sınıf = ogrsınıfbul(textBox5.Text);
            derskod = derskodbul(comboBox9.Text,sınıf);      
            DialogResult cevap = MessageBox.Show(textBox5.Text + " numaralı kişiyi Kaydetmek İstıyonmu öglim  ??? ", "Uyarı Mesajı", MessageBoxButtons.YesNoCancel);
            if (cevap == DialogResult.Yes)
            {
                bag.Open();
                OleDbCommand komut = new OleDbCommand();
                komut.Connection = bag;
                komut.CommandText = "insert into notlar (Ogrno,derskod,s1,s2,p1,p2)values('" + textBox5.Text + "','" + derskod + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox10.Text + "','" + textBox9.Text + "')";
                komut.ExecuteNonQuery();
                bag.Close();             
            }
            else
            {
                MessageBox.Show("İptal edildi");
            }
        }
    }
}
//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**
//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**
//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**
//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**
//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**
//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**
//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**
//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**