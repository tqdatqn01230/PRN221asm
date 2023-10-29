using BusinessObject.Models;
using BusinessObject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for UpdateMemberWindow.xaml
    /// </summary>
    public partial class UpdateMemberWindow : Window
    {
        IMemberRepository memberRepository;
        WindowManagerMembers windowManagerMembers;
        Member member;
        bool ischeck = false;
        public UpdateMemberWindow(IMemberRepository repository, WindowManagerMembers wmm, Member members)
        {
            InitializeComponent();
            memberRepository = repository;
            windowManagerMembers = wmm;
            member = members;
            txtCity.Text = members.City;
            txtCountry.Text = members.Country;
            txtCompanyName.Text = members.CompanyName;
            txtEmail.Text = members.Email;
            txtMemberId.Text = "" + members.MemberId;
            txtPassword.Text = members.Password;
            txtEmail.IsReadOnly = true;
            txtPassword.IsReadOnly = false;
        }

        public UpdateMemberWindow(IMemberRepository repository, Member members, bool check)
        {
            InitializeComponent();
            memberRepository = repository;
            member = members;
            txtCity.Text = members.City;
            txtCountry.Text = members.Country;
            txtCompanyName.Text = members.CompanyName;
            txtEmail.Text = members.Email;
            txtMemberId.Text = "" + members.MemberId;
            txtPassword.Text = members.Password;
            ischeck = check;
            txtEmail.IsReadOnly = true;
            txtPassword.IsReadOnly = false;
        }
        private Member GetMemberObjects()
        {
            Member member = null;
            try
            {
                member = new Member
                {
                    MemberId = int.Parse(txtMemberId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Member");
            }

            return member;
        }

        public UpdateMemberWindow()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Member members = GetMemberObjects();
                bool check = memberRepository.GetMembers().Any(x => x.Email == txtEmail.Text.Trim() && members.MemberId != x.MemberId);
                if (check)
                {
                    MessageBox.Show($"{member.Email} is duplicated! ", "Update Member");
                    return;
                }
                memberRepository.UpdateMember(members);
                
                if (ischeck)
                {
                    OrderRepository orderRepository = new OrderRepository();
                    var managerOrderCustomerWindow = new ManagerOrderCustomerWindow(orderRepository, members);
                    managerOrderCustomerWindow.Show();
                    managerOrderCustomerWindow.LoadOrderList();
                }
                else
                {
                    windowManagerMembers.LoadMemberList();
                }
                this.Close();
                MessageBox.Show($"{member.Email} updated successfully ", "Update Member");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update car");
            }
        }
    }
}
