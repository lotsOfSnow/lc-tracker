<script lang="ts">
  import { type ClassName, cn } from '$lib/components/componentUtils';

  let className: ClassName = undefined;
  export { className as class };
</script>

<button {...$$restProps} on:click
        class={cn("bg-gray-700 hover:bg-gray-600 text-white font-medium py-2 px-4 rounded-lg transition duration-300 ease-in-out", className)}>
  <slot />
</button>
