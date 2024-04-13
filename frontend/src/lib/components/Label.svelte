<script>
  import { cn } from '$lib/components/componentUtils';

  let className = undefined;
  export { className as class };
</script>

<label {...$$restProps} class={cn("block text-sm font-medium text-gray-700", className)}>
  <slot />
</label>
