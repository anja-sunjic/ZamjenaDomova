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

namespace ZamjenaDomova.WinUI.Amenities
{
    public partial class ucAmenities : UserControl
    {
        private readonly APIService _amenityService = new APIService("Amenity");
        private readonly APIService _amenityCategoryService = new APIService("AmenitiesCategory");

        public ucAmenities()
        {
            InitializeComponent();
            ReloadDataGridView();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
           ReloadDataGridView();
        }

        private async Task LoadNewAmenityCategories()
        {
            var result = await _amenityCategoryService.Get<List<Model.AmenitiesCategory>>(null);
            result.Insert(0, new Model.AmenitiesCategory { AmenitiesCategoryId = null, Name = null });

            cmbNewCategory.DataSource = result;
            cmbNewCategory.DisplayMember = "Name";
            cmbNewCategory.ValueMember = "AmenitiesCategoryId";
        }
        private async Task LoadSearchAmenityCategories()
        {
            var result = await _amenityCategoryService.Get<List<Model.AmenitiesCategory>>(null);
            result.Insert(0, new Model.AmenitiesCategory { AmenitiesCategoryId = null, Name = null });

            cmbSearchCategory.DataSource = result;
            cmbSearchCategory.DisplayMember = "Name";
            cmbSearchCategory.ValueMember = "AmenitiesCategoryId";

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            AmenityUpsertRequest request = new AmenityUpsertRequest();

            request.Name = txtNewName.Text;
            var idObj = cmbNewCategory.SelectedValue;
            if (int.TryParse(idObj.ToString(), out int id))
            {
                request.AmenitiesCategoryId = id;
            }

            await _amenityService.Insert<Model.Amenity>(request);
            MessageBox.Show("Uspjesno!");

            txtNewName.Text = "";
            cmbNewCategory.SelectedIndex = 0;

            ReloadDataGridView();

        }
        public async void ReloadDataGridView()
        {
            var search = new AmenitySearchRequest { Name = txtSearchName.Text };
            var idObj = cmbSearchCategory.SelectedValue;
            if (idObj != null && int.TryParse(idObj.ToString(), out int id))
            {
                search.AmenitiesCategoryId = id;
            }
            var result = await _amenityService.Get<List<Model.Amenity>>(search);
            dgvAmenities.AutoGenerateColumns = false;
            dgvAmenities.DataSource = result;

        }

        private void dgvAmenities_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var id = dgvAmenities.SelectedRows[0].Cells["AmenityId"].Value;
            frmAmenityDetails frm = new frmAmenityDetails(int.Parse(id.ToString()));
            frm.Show();
        }

        private async void ucAmenities_Load(object sender, EventArgs e)
        {
            await LoadNewAmenityCategories();
            await LoadSearchAmenityCategories();

            var result = await _amenityService.Get<List<Model.Amenity>>(null);
            dgvAmenities.AutoGenerateColumns = false;
            dgvAmenities.DataSource = result;
        }

    }
}
