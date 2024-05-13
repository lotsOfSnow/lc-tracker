<script lang="ts">
  import { type ClassName, cn } from '$lib/components/componentUtils';

  let className: ClassName | undefined = undefined;
  export { className as class };

  export let title: string | undefined = undefined;
</script>

<label {...$$restProps} class={cn("block text-sm font-medium text-gray-700", className)} {title}>
  <slot />
</label>
