# ns-ddfe-core

Esta biblioteca possibilita a comunicação e o consumo da solução API para DDFe da NS Tecnologia.

Para imlementar esta bibilioteca em seu projeto, você pode:

1. Realizar a instalação do [pacote](https://www.nuget.org/packages/ns-ddfe-core/) através do Microsoft NuGet no Visual Studio

![Screenshot_4](https://user-images.githubusercontent.com/49299197/135635665-bfb55be5-59e6-4f1e-80dc-0992787c66bf.jpg)

3. Realizar o download da biblioteca pelo [GitHub](https://github.com/konflanzzz/ns-nfe-core/archive/refs/heads/main.zip) e adicionar a pasta "src" em seu projeto no C# (.NET Core )

# Exemplos de uso do pacote

Apos instalação através do gerenciador de pacotes NuGet, faça referência dela em seu projeto:

    using ns_ddfe_core;

Para que a comunicação com a API possa ser feita, é necessário informar o seu Token no cabeçalho das requisicoes. 
Com este pacote, você pode fazê-lo assim como no exemplo:

    configParceiro.token = "4dec0a34f460169dd6fb2ef9193003e0"

## Download Unico

## Download em Lote

### Informações Adicionais

Para saber mais sobre o projeto NFe API da NS Tecnologia, consulte a [documentação](https://docsnstecnologia.wpcomstaging.com/docs/ns-ddfe/)
