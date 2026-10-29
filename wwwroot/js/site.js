document.addEventListener('DOMContentLoaded', () => {
  // Keep room cards graceful if an external photo provider is unavailable.
  document.querySelectorAll('.room-image, .booking-summary img, .confirm-room img, .my-booking img').forEach(img => {
    img.addEventListener('error', () => {
      img.style.backgroundColor = '#dfe4da';
      img.style.objectFit = 'cover';
      img.removeAttribute('src');
      img.alt = 'Room photo temporarily unavailable';
    }, { once: true });
  });
  const checkIn = document.querySelector('input[name="CheckIn"]');
  const checkOut = document.querySelector('input[name="CheckOut"]');
  if (checkIn && checkOut) {
    const updateMinimum = () => {
      if (checkIn.value) {
        const date = new Date(`${checkIn.value}T00:00:00`);
        date.setDate(date.getDate() + 1);
        const min = `${date.getFullYear()}-${String(date.getMonth()+1).padStart(2,'0')}-${String(date.getDate()).padStart(2,'0')}`;
        checkOut.min = min;
        if (checkOut.value && checkOut.value < min) checkOut.value = min;
      }
    };
    checkIn.addEventListener('change', updateMinimum);
    updateMinimum();
  }
});
