<script lang="ts" generics="T">
  import type { Column } from '$lib/components/table/column';

  export let columns: Record<string, Column<T>>;
  export let data: T[] | null | undefined;
</script>

<table class="min-w-full leading-normal">
  <thead>
  <tr>
    {#each Object.values(columns) as col}
      <th
        class="first:rounded-tl-lg last:rounded-tr-lg px-5 py-3 border-b-2 border-gray-300 bg-gray-200 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider"
      >{col.title}</th
      >
    {/each}
  </tr>
  </thead>
  <tbody>
  {#each data ?? [] as row}
    <tr class="">
      {#each Object.values(columns) as col}
        <td
          class="max-w-20 px-5 py-2 border-b border-gray-200 bg-white text-sm truncate"
        >
          <slot {row} {col} />
        </td>
      {/each}
    </tr>
  {/each}
  </tbody>
</table>
