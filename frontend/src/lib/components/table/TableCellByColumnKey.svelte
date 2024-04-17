<script lang="ts" generics="T">
  import type { Column } from '$lib/components/table/column';

  export let value: T;
  export let col: Column<T>;
</script>

<p
  class="text-gray-900 whitespace-no-wrap overflow-hidden overflow-ellipsis"
  title={value[col.key]?.toString()}
>
  {value[col.key]}
</p>
