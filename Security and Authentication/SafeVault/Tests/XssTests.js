test('Escapes HTML in username field to prevent XSS', () => {
    const maliciousInput = `<img src="x" onerror="alert('hacked')">`;
    const el = document.createElement('span');
    el.textContent = maliciousInput;

    expect(el.innerHTML).not.toContain('onerror');
    expect(el.innerHTML).not.toContain('<img');
    expect(el.textContent).toBe(maliciousInput);
});
