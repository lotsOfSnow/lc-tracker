<script lang="ts">
  import { enhance } from '$app/forms';
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

  const columns: { [key: string]: Column<apiSchemas['Attempt']> } = {
    number: {
      key: 'date',
      title: 'Date',
    },
    name: {
      key: 'hasSolved',
      title: 'Solved?',
    },
    isRecap: {
      key: 'isRecap',
      title: 'Recap?',
    },
    difficulty: {
      key: 'difficulty',
      title: 'Difficulty',
    },
    actions: {
      title: 'Actions',
    },
  };

</script>

<Box width="big">
  <LinkButton href={AppRoute.ATTEMPTS_NEW}>Create new</LinkButton>

  <BoxHeader>Attempts</BoxHeader>

  <div class="mt-4 overflow-x-auto">
    <Table {columns} let:columns>
      <TableRowTemplate {columns} values={data.value} let:value={attempt} let:col>
        {#if col?.key}
          {#if col.key === 'hasSolved'}
            <span
              class={`px-6 py-1 rounded-lg
              ${attempt.hasSolved ? 'bg-green-500' : 'bg-red-500'}
               font-semibold text-white cursor-default`}>
              {attempt.hasSolved ? 'YES' : 'NO'}
            </span>
          {:else}
            <TableCellByColumnKey {col} value={attempt} />
          {/if}
        {:else}
          <div class="flex justify-end space-x-1">
            <TableEditActionButton href="{AppRoute.ATTEMPTS}/{attempt.id}" />
            <form method="POST" on:submit|preventDefault use:enhance action="?/deleteAttempt">
              <input name="id" value={attempt.id} hidden />
              <button
                class="bg-red-600 hover:bg-red-500 text-white font-medium py-1 px-2 rounded transition duration-300 ease-in-out"
              >
                Delete
              </button>
            </form>

          </div>
        {/if}
      </TableRowTemplate>
    </Table>
  </div>
</Box>
