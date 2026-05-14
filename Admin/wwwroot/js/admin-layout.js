document.addEventListener('DOMContentLoaded', function () {
    const trigger = document.getElementById('userMenuTrigger');
    const menu = document.getElementById('userDropdownMenu');

    if (trigger && menu) {
        // 1. Click vào avatar để ẩn/hiện
        trigger.addEventListener('click', function (e) {
            e.stopPropagation();
            menu.classList.toggle('hidden');
        });

        // 2. Click ra ngoài để đóng menu
        window.addEventListener('click', function (e) {
            if (!menu.contains(e.target) && !trigger.contains(e.target)) {
                menu.classList.add('hidden');
            }
        });

        // 3. Nhấn ESC để đóng menu
        window.addEventListener('keydown', function (e) {
            if (e.key === 'Escape') {
                menu.classList.add('hidden');
            }
        });
    }
});