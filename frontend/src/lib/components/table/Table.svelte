<script lang="ts" generics="T">
  import type { Column } from '$lib/components/table/column';

  export let columns: Record<string, Column<T>>;
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
  <slot {columns} />
</table>
