<script lang="ts">
  import { AppRoute } from '$lib/routes';
  import LinkButton from '$lib/components/LinkButton.svelte';
  import type { apiSchemas } from '$lib/api';
  import type { Column } from '$lib/components/table/column';
  import Table from '$lib/components/table/Table.svelte';
  import TableRowTemplate from '$lib/components/table/TableRowTemplate.svelte';
  import TableCellByColumnKey from '$lib/components/table/TableCellByColumnKey.svelte';
  import TableEditActionButton from '$lib/components/table/TableEditActionButton.svelte';
  import Box from '$lib/components/box/Box.svelte';
  import BoxHeader from '$lib/components/box/BoxHeader.svelte';

  export let data;

  const columns: { [key: string]: Column<apiSchemas['Problem']> } = {
    name: {
      key: 'name',
      title: 'Name',
    },
    actions: {
      title: 'Actions',
    },
  };
</script>

<Box width="big">
  <LinkButton href={AppRoute.PROBLEMS_NEW}>Create new</LinkButton>

  <BoxHeader>Problems</BoxHeader>

  <div class="mt-4 overflow-x-auto">
    <Table {columns} let:columns>
      <TableRowTemplate columns={columns} values={data.value} let:value={problem} let:col>
        {#if col?.key}
          <TableCellByColumnKey {col} value={problem} />
        {:else}
          <div class="flex justify-end space-x-1">
            <TableEditActionButton href="{AppRoute.PROBLEMS}/{problem.id}" />
          </div>
        {/if}
      </TableRowTemplate>
    </Table>
  </div>
</Box>
<!-- TODO: Pagination, filtering, sorting -->
