namespace WebBanHang.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductDetail")]
    public partial class ProductDetail
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = " Thuộc tính (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string Properties { get; set; }

        [StringLength(500)]
        [Display(Name = "Giá trị")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public string Value { get; set; }

        [Display(Name = "Tên Sản phẩm (*)")]
        [Required(ErrorMessage = "Trường này không được rỗng!")]
        public long? ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}