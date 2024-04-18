<script lang="ts">
  import { AppRoute } from '$lib/routes';
  import LinkButton from '$lib/components/LinkButton.svelte';
  import type { apiSchemas } from '$lib/api';
  import type { Column } from '$lib/components/table/column';
  import Table from '$lib/components/table/Table.svelte';
  import TableRowTemplate from '$lib/components/table/TableRowTemplate.svelte';
  import TableCellByColumnKey from '$lib/components/table/TableCellByColumnKey.svelte';
  import TableEditActionButton from '$lib/components/table/TableEditActionButton.svelte';

  export let data;

  const columns: { [key: string]: Column<apiSchemas['Attempt']> } = {
    number: {
      key: 'date',
      title: 'Date',
    },
    name: {
      key: 'hasSolved',
      title: 'Solved?',
    },
    url: {
      key: 'note',
      title: 'Note',
    },
    actions: {
      title: 'Actions',
    },
  };
</script>

<div class="bg-gray-100 p-6 w-full max-w-6xl mx-auto rounded-lg shadow-md">
  <LinkButton href={AppRoute.ATTEMPTS_NEW}>Create new</LinkButton>

  <h2 class="text-xl font-semibold text-gray-800">Attempts</h2>

  <div class="mt-4 overflow-x-auto">
    <Table {columns} let:columns>
      <TableRowTemplate columns={columns} values={data.value} let:value={attempt} let:col>
        {#if col?.key}
          <TableCellByColumnKey {col} value={attempt} />
        {:else}
          <div class="flex justify-end space-x-1">
            <TableEditActionButton href="{AppRoute.ATTEMPTS}/{attempt.id}" />
            <button
              class="bg-red-600 hover:bg-red-500 text-white font-medium py-1 px-2 rounded transition duration-300 ease-in-out"
            >
              Delete
            </button>
          </div>
        {/if}
      </TableRowTemplate>
    </Table>
  </div>
</div>
