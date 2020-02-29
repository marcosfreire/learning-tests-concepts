## DESENVOLVIMENTO DO DOMINIO DE VENDAS

#### PEDIDO - ITEM PEDIDO - VOUCHER

```
Um item de um pedido representa um produto e pode conter mais de uma unidade
Independente da ação, um item precisa ser sempre valido:
Possuir: Id e Nome do produto, quantidade entre 1 e 15 unidades, valor maior que 0
```

Um pedido enquanto não iniciado (processo de pagamento) está no estado de rascunho
e deve pertencer a um cliente.

#### Adicionar Item                
- Ao adicionar um item é necessário calcular o valor total do pedido 
- Se um item já está na lista então deve acrescer a quantidade do item no pedido
- O item deve ter entre 1 e 15 unidades do produto

#### Atualizacao de Item
- O item precisa estar na lista para ser atualizado
- Um item pode ser atualizado contendo mais ou menos unidades do que anteriormente                
- Ao atualizar um item é necessário calcular o valor total do pedido 
- Um item deve permanecer entre 1 e 15 unidades do produto

#### Remoção de Item
- O item precisa estar na lista para ser removido
- Ao remover um item é necessário calcular o valor total do pedido 

```
Um voucher possui um código único e o desconto pode ser em percentual ou valor fixo
Usar uma flag indicando que um pedido teve um voucher de desconto aplicado e o valor do desconto gerado
```

#### Aplicar voucher de desconto
- O voucher só pode ser aplicado se estiver válido, para isto:
- Deve possuir um código            
- A data de validade é superior a data atual
- O voucher está ativo
- O voucher possui quantidade disponivel
- Uma das formas de desconto devem estar preenchidas com valor acima de 0
- Calcular o desconto conforme tipo do voucher
- Voucher com desconto percentual
- Voucher com desconto em valores (reais)
- Quando o valor do desconto ultrapassa o total do pedido o pedido recebe o valor: 0
- Após a aplicação do voucher o desconto deve ser re-calculado após toda modificação da lista
de itens do pedido

### PEDIDO COMMANDS - HANDLER

O command handler de pedido irá manipular um command para cada intenção em relação ao pedido. 
Em todos os commandos manipulados devem ser verificados:

- Se o command é válido
- Se o pedido existe
- Se o item do pedido existe      

#### Na alteração de estado do pedido:

- Deve ser feita via repositório
- Deve enviar um evento

#### AdicionarItemPedidoCommand
- Verificar se é um pedido novo ou em andamento
- Verificar se o item já foi adicionado a lista
