# Match3Game-HM-Games---Test

# Đánh giá dự án

- Các UIPanel để ở scene nhiều sẽ nặng cho scene và Việc get m_menuList rất nặng. Mình có thể chuyển vào resource rồi load ra và cache lại dùng.
- Thay vì việc Instantiate và destroy Item thì mình có thể dùng pooling.
- Texture thì nên xuất ra với kích thước chiều dài và rộng đều chia hết cho 4.

* Dự án chia các class, Interface rất rõ ràng. Dễ dàng mở rộng thêm các tính năng.
