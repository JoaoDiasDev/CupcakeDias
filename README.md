### English

# CupcakeDias - Cupcake Selling System

CupcakeDias is a web application designed to manage cupcake sales, including browsing available cupcakes, adding them to the cart, managing orders, and providing an admin interface for managing ingredients, cupcakes, and orders.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Setup Instructions](#setup-instructions)
- [Components Overview](#components-overview)
  - [Cupcake List](#cupcake-list)
  - [Cart](#cart)
  - [Order Management](#order-management)
  - [Admin Section](#admin-section)
- [Usage](#usage)
  - [Viewing Cupcakes](#viewing-cupcakes)
  - [Managing the Cart](#managing-the-cart)
  - [Placing Orders](#placing-orders)
  - [Admin Operations](#admin-operations)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Cupcake Listing**: Users can browse a list of cupcakes with detailed information such as name, base flavor, price, and ingredients.
- **Search Functionality**: Users can search for cupcakes by name, making it easier to find desired products.
- **Cart Management**: Add cupcakes to a cart with customizable quantities, view the cart, remove items, and proceed to checkout.
- **Order Tracking**: Customers can view their previous orders and check the status of current orders.
- **Admin Panel**: Admins can manage ingredients, cupcakes, and orders. This includes adding/editing/deleting cupcakes and ingredients, as well as viewing and managing all orders.

## Technologies

- **Angular** for the front-end application framework.
- **Angular Material** for UI components and styling.
- **RxJS** for handling asynchronous data streams.
- **Ngx-Currency** for handling currency input and formatting.
- **REST API** for backend services.

## Setup Instructions

### Prerequisites

Ensure that the following are installed on your local machine:

- **Node.js** (v20.x or higher)
- **Angular CLI** (v18+)
- **npm** (v10 or higher)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/JoaoDiasDev/CupcakeDias.git
   cd CupcakeDias\src\frontend\cupcake-dias-ui
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Run the development server:

   ```bash
   ng serve
   ```

4. Open your browser and go to `http://localhost:4200`.

## Components Overview

### Cupcake List

The **Cupcake List Component** is responsible for displaying all available cupcakes. It allows users to search for specific cupcakes by name and add them to the cart.

- **Search Filter**: Users can search by cupcake name using the search bar.
- **Add to Cart**: For each cupcake, users can select a quantity and add it to the cart.

#### Relevant Files:

- `src/app/components/cupcake-list/cupcake-list.component.ts`
- `src/app/components/cupcake-list/cupcake-list.component.html`
- `src/app/components/cupcake-list/cupcake-list.component.css`

### Cart

The **Cart Component** allows users to view all cupcakes that have been added to their cart, modify quantities, remove items, and proceed to checkout.

- **View Cart**: Shows all cupcakes in the cart with the option to update quantities or remove items.
- **Checkout**: Proceeds to the checkout process, confirming the order.

#### Relevant Files:

- `src/app/components/cart/cart.component.ts`
- `src/app/components/cart/cart.component.html`
- `src/app/services/cart.service.ts`

### Order Management

Users can view a list of their orders and track their status through the **Order Management Component**.

- **Order History**: Displays a list of previous orders, showing the date, total cost, and status.
- **Order Details**: For each order, users can view more detailed information.

#### Relevant Files:

- `src/app/components/orders/orders.component.ts`
- `src/app/services/orders.service.ts`

### Admin Section

The **Admin Section** allows administrators to manage all aspects of the cupcake system. This includes managing ingredients, cupcakes, and orders.

- **Manage Cupcakes**: Admins can create, update, and delete cupcakes from the system.
- **Manage Ingredients**: Admins can manage the list of available ingredients, which can be assigned to cupcakes.
- **Manage Orders**: Admins can view and manage all customer orders, including changing order statuses.

#### Relevant Files:

- `src/app/components/admin/admin-cupcakes/admin-cupcakes.component.ts`
- `src/app/components/admin/admin-ingredients/admin-ingredients.component.ts`
- `src/app/components/admin/admin-orders/admin-orders.component.ts`
- `src/app/services/admin.service.ts`

## Usage

### Viewing Cupcakes

1. Navigate to the **Cupcake List** page to view all available cupcakes.
2. Use the search bar to filter cupcakes by name.
3. For each cupcake, you can view the name, flavor, price, description, and ingredients.

### Managing the Cart

1. Add cupcakes to your cart from the **Cupcake List** by selecting the desired quantity and clicking "Add to Cart."
2. Navigate to the **Cart** page to view all added items.
3. Adjust quantities, remove items, or proceed to checkout.

### Placing Orders

1. After reviewing the cart, proceed to checkout.
2. Confirm the order details and complete the purchase.
3. Once the order is placed, you can track it in the **Orders** section.

### Admin Operations

1. Admin users can log in to the **Admin Section**.
2. Manage cupcakes, ingredients, and orders through the respective admin panels.

## Contributing

We welcome contributions to improve CupcakeDias! Please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix:
   ```bash
   git checkout -b feature/new-feature
   ```
3. Make your changes and commit them:
   ```bash
   git commit -m "Add new feature"
   ```
4. Push to your branch:
   ```bash
   git push origin feature/new-feature
   ```
5. Open a pull request on GitHub.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

### PT-BR

# CupcakeDias - Sistema de Venda de Cupcakes

CupcakeDias é uma aplicação web desenvolvida para gerenciar a venda de cupcakes, permitindo que os usuários visualizem os produtos, adicionem ao carrinho, façam pedidos e, no caso de administradores, gerenciem ingredientes, cupcakes e pedidos.

## Índice

- [Funcionalidades](#funcionalidades)
- [Tecnologias](#tecnologias)
- [Instruções de Configuração](#instruções-de-configuração)
- [Visão Geral dos Componentes](#visão-geral-dos-componentes)
  - [Lista de Cupcakes](#lista-de-cupcakes)
  - [Carrinho](#carrinho)
  - [Gerenciamento de Pedidos](#gerenciamento-de-pedidos)
  - [Seção de Administração](#seção-de-administração)
- [Uso](#uso)
  - [Visualizando Cupcakes](#visualizando-cupcakes)
  - [Gerenciando o Carrinho](#gerenciando-o-carrinho)
  - [Realizando Pedidos](#realizando-pedidos)
  - [Operações de Administrador](#operações-de-administrador)
- [Contribuindo](#contribuindo)
- [Licença](#licença)

## Funcionalidades

- **Listagem de Cupcakes**: Os usuários podem visualizar uma lista de cupcakes com informações detalhadas como nome, sabor, preço e ingredientes.
- **Funcionalidade de Pesquisa**: Os usuários podem pesquisar cupcakes pelo nome.
- **Gerenciamento de Carrinho**: Adicionar cupcakes ao carrinho com quantidades personalizadas, visualizar o carrinho, remover itens e prosseguir para o checkout.
- **Acompanhamento de Pedidos**: Os clientes podem visualizar seus pedidos anteriores e acompanhar o status dos pedidos atuais.
- **Painel de Administração**: Administradores podem gerenciar ingredientes, cupcakes e pedidos, incluindo adicionar, editar e excluir produtos e gerenciar os pedidos dos clientes.

## Tecnologias

- **Angular** como framework para o front-end.
- **Angular Material** para componentes de UI e estilização.
- **RxJS** para lidar com fluxos de dados assíncronos.
- **Ngx-Currency** para entrada e formatação de moeda.
- **API REST** para os serviços de backend.

## Instruções de Configuração

### Pré-requisitos

Certifique-se de ter os seguintes softwares instalados em sua máquina:

- **Node.js** (v20.x+)
- **Angular CLI** (v18+)
- **npm** (v10+)

### Instalação

1. Clone o repositório:

   ```bash
   git clone https://github.com/JoaoDiasDev/CupcakeDias.git
   cd CupcakeDias
   ```

2. Instale as dependências:

   ```bash
   npm install
   ```

3. Execute o servidor de desenvolvimento:

   ```bash
   ng serve
   ```

4. Abra o navegador e acesse `http://localhost:4200`.

## Visão Geral dos Componentes

### Lista de Cupcakes

O **Componente de Lista de Cupcakes** exibe todos os cupcakes disponíveis. Ele permite que os usuários pesquisem cupcakes pelo nome e adicionem ao carrinho.

- **Filtro de Pesquisa**: Os usuários podem pesquisar cupcakes pelo nome na barra de pesquisa.
- **Adicionar ao Carrinho**: Para cada cupcake, o usuário pode selecionar a quantidade e adicionar ao carrinho.

#### Arquivos Relevantes:

- `src/app/components/cupcake-list/cupcake-list.component.ts`
- `src/app/components/cupcake-list/cupcake-list.component.html`
- `src/app/components/cupcake-list/cupcake-list.component.css`

### Carrinho

O **Componente de Carrinho** permite que os usuários visualizem todos os cupcakes adicionados ao carrinho, modifiquem as quantidades, removam itens e prossigam para o checkout.

- **Visualizar Carrinho**: Exibe todos os cupcakes no carrinho com a opção de atualizar quantidades ou remover itens.
- **Checkout**: O usuário pode prosseguir para a finalização da compra, confirmando o pedido.

#### Arquivos Relevantes:

- `src/app/components/cart/cart.component.ts`
- `src/app/components/cart/cart.component.html`
- `src/app/services/cart.service.ts`

### Gerenciamento de Pedidos

Os usuários podem visualizar uma lista de seus pedidos e acompanhar o status de cada um através do **Componente de Pedidos**.

- **Histórico de Pedidos**: Exibe uma lista de pedidos anteriores, mostrando a data, custo total e status.
- **Detalhes do Pedido**: Permite que o usuário visualize informações detalhadas sobre cada pedido.

#### Arquivos Relevantes:

- `src/app/components/orders/orders.component.ts`
- `src/app/services/orders.service.ts`

### Seção de Administração

A **Seção de Administração** permite que os administradores gerenciem todos os aspectos do sistema de cupcakes, incluindo ingredientes, cupcakes e pedidos.

- **Gerenciar Cupcakes**: Administradores podem criar, atualizar e excluir cupcakes do sistema.
- **Gerenciar Ingredientes**: Administradores podem gerenciar a lista de ingredientes disponíveis, que podem ser atribuídos aos cupcakes.
- **Gerenciar Pedidos**: Administradores podem visualizar e gerenciar todos os pedidos dos clientes, incluindo a alteração do status do pedido.

#### Arquivos Relevantes:

- `src/app/components/admin/admin-cupcakes/admin-cupcakes.component.ts`
- `src/app/components/admin/admin-ingredients/admin-ingredients.component.ts`
- `src/app/components/admin/admin-orders/admin-orders.component.ts`
- `src/app/services/admin.service.ts`

## Uso

### Visualizando Cupcakes

1. Navegue até a página **Lista de Cupcakes** para visualizar todos os cupcakes disponíveis.
2. Use a barra de pesquisa para filtrar cupcakes pelo nome.
3. Para cada cupcake, você pode visualizar o nome, sabor, preço, descrição e ingredientes.

### Gerenciando o Carrinho

1. Adicione cupcakes ao seu carrinho na **Lista de Cupcakes**, selecionando a quantidade desejada e clicando em "Adicionar ao Carrinho".
2. Navegue até a página **Carrinho** para visualizar todos os itens adicionados.
3. Ajuste as quantidades, remova itens ou prossiga para o checkout.

### Realizando Pedidos

1. Após revisar o carrinho, prossiga para o checkout.
2. Confirme os detalhes do pedido e finalize a compra.
3. Após o pedido ser realizado, você pode acompanhá-lo na seção **Pedidos**.

### Operações de Administrador

1. Administradores podem fazer login na **Seção de Administração**.
2. Gerencie cupcakes, ingredientes e pedidos através dos respectivos painéis de administração.

## Contribuindo

Contribuições são bem-vindas! Por favor, siga os seguintes passos:

1. Faça um fork do repositório.
2. Crie uma nova branch para sua funcionalidade ou correção de bug:
   ```bash
   git checkout -b feature/nova-funcionalidade
   ```
3. Faça suas alterações e faça commit delas:
   ```bash
   git commit -m "Adiciona nova funcionalidade"
   ```
4. Envie sua branch:
   ```bash
   git push origin feature/nova-funcionalidade
   ```
5. Abra um pull request no GitHub.

## Licença

Este projeto está licenciado sob a Licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.

---
