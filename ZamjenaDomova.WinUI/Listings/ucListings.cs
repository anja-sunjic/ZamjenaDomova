﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZamjenaDomova.Model.Requests;
using ZamjenaDomova.Model;

namespace ZamjenaDomova.WinUI.Listings
{
    public partial class ucListings : UserControl
    {
        private readonly APIService _listingService = new APIService("Listing");
        bool IsStartDateNull = true;
        bool IsEndDateNull = true;

        public ucListings()
        {
            InitializeComponent();
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = " ";
            dtpStart.CustomFormat = " ";
            dgvListings.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvListings.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvListings.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void dgvListings_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var id = dgvListings.SelectedRows[0].Cells[0].Value;
            frmListingDetails frm = new frmListingDetails(int.Parse(id.ToString()), true);
            frm.Show();
        }

        private async void ucListings_Load(object sender, EventArgs e)
        {
            var request = new ListingSearchRequest { Approved = true };

            var result = await _listingService.Get<List<Listing>>(request);
            dgvListings.AutoGenerateColumns = false;
            dgvListings.DataSource = result;

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var request = new ListingSearchRequest
            {
                Approved = true,
                City = txtCity.Text,
            };

            if (IsStartDateNull)
                request.StartDate = null;
            else request.StartDate = dtpStart.Value;

            if (IsEndDateNull)
                request.EndDate = null;
            else request.EndDate = dtpEnd.Value;

            var result = await _listingService.Get<List<Listing>>(request);
            dgvListings.AutoGenerateColumns = false;
            dgvListings.DataSource = result;
        }

        private void lblClear_MouseClick(object sender, MouseEventArgs e)
        {
            dtpEnd.CustomFormat = " ";
            dtpStart.CustomFormat = " ";
            IsStartDateNull = true;
            IsEndDateNull = true;
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            dtpStart.CustomFormat = "dd/MM/yyyy";
            IsStartDateNull = false;
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            dtpEnd.CustomFormat = "dd/MM/yyyy";
            IsEndDateNull = false;
        }

    }
}
