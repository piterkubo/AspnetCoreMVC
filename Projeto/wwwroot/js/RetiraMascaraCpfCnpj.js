function removePontos(input) {
    input.value = input.value.replace(/[.-]/g, '');
    input.value = input.value.replace(/[./-]/g, '');
}

/*function removePontosCNPJ(input) {
    input.value = input.value.replace(/[./-]/g, '');
}*/