// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
  $('#myTable').DataTable({
      "info": false,
      "paging": false,
      "searching": false,
      "language": {
        "search": "Tìm kiếm:",
        "infoFiltered": "(đã lọc từ _MAX_ bản ghi)",
        "zeroRecords": "Không tìm thấy dữ liệu phù hợp",
        "sLengthMenu": "Hiển thị _MENU_ bản ghi",
        "paginate": {
            "previous": "Trước",
            "next": "Sau"
        },
    }
  });
});

document.addEventListener('DOMContentLoaded', function () {
  const toggleSidebarButton = document.getElementById('toggleSidebar');
  const sidebar = document.getElementById('sidebar');

  toggleSidebarButton.addEventListener('click', function () {
      sidebar.classList.toggle('show');
  });

  // Lấy tất cả các nút "Chi tiết" và thêm sự kiện click cho mỗi nút
  const detailButtons = document.querySelectorAll('.btn-details');
  detailButtons.forEach(function (button) {
      button.addEventListener('click', function (event) {
          // Ngăn chặn sự kiện click từ lan truyền đến các phần khác của trang
          event.stopPropagation();

          // Gọi hàm openModal với ID tương ứng
          openModal(button.getAttribute('data-id'));
          openYeuCauModal(button.getAttribute('data-id'));
      });
  });

  function openModal(id) {
      // Sử dụng AJAX để gửi yêu cầu lấy thông tin chi tiết từ server
      $.ajax({
          url: '/BangDiem/Details/' + id,
          type: 'GET',
          success: function (data) {
              // Nạp dữ liệu vào modal
              $('#modalContent').html(data);
              // Mở modal
              $('#myModal').modal('show');
          },
          // error: function () {
          //     alert('Đã xảy ra lỗi khi tải dữ liệu chi tiết.');
          // }
      });
      $('#myModal .close, #myModal [data-dismiss="modal"]').on('click', function () {
        $('#myModal').modal('hide');
    });
  }
    function openYeuCauModal(id) {
      $.ajax({
          url: '/YeuCauSuaDiem/Details/' + id,
          type: 'GET',
          success: function (data) {
              $('#yeuCauModalContent').html(data);
              $('#yeuCauModal').modal('show');
          },
          // error: function () {
          //     alert('Đã xảy ra lỗi khi tải dữ liệu yêu cầu sửa điểm.');
          // }
      });
      $('#yeuCauModal .close, #yeuCauModal [data-dismiss="modal"]').on('click', function () {
        $('#yeuCauModal').modal('hide');
    });
  }
});

// function sortTable(columnIndex) {
//     $('#myTable').DataTable().order([columnIndex, $('#myTable').DataTable().order()[0][1] === 'asc' ? 'desc' : 'asc']).draw();
// }
// function sortTable(n) {
//     var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
//     table = document.getElementById("myTable");
//     switching = true;
//     //Set the sorting direction to ascending:
//     dir = "asc"; 
//     /*Make a loop that will continue until
//     no switching has been done:*/
//     while (switching) {
//       //start by saying: no switching is done:
//       switching = false;
//       rows = table.rows;
//       /*Loop through all table rows (except the
//       first, which contains table headers):*/
//       for (i = 1; i < (rows.length - 1); i++) {
//         //start by saying there should be no switching:
//         shouldSwitch = false;
//         /*Get the two elements you want to compare,
//         one from current row and one from the next:*/
//         x = rows[i].getElementsByTagName("TD")[n];
//         y = rows[i + 1].getElementsByTagName("TD")[n];
//         /*check if the two rows should switch place,
//         based on the direction, asc or desc:*/
//         if (dir == "asc") {
//           if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
//             //if so, mark as a switch and break the loop:
//             shouldSwitch= true;
//             break;
//           }
//         } else if (dir == "desc") {
//           if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
//             //if so, mark as a switch and break the loop:
//             shouldSwitch = true;
//             break;
//           }
//         }
//       }
//       if (shouldSwitch) {
//         /*If a switch has been marked, make the switch
//         and mark that a switch has been done:*/
//         rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
//         switching = true;
//         //Each time a switch is done, increase this count by 1:
//         switchcount ++;      
//       } else {
//         /*If no switching has been done AND the direction is "asc",
//         set the direction to "desc" and run the while loop again.*/
//         if (switchcount == 0 && dir == "asc") {
//           dir = "desc";
//           switching = true;
//         }
//       }
//     }
// }
  
