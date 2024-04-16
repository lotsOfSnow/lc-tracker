<script>
  import { cn } from '$lib/components/componentUtils';

  let className = undefined;
  export { className as class };
</script>

<a {...$$restProps}
   class={cn("inline-block bg-gray-700 hover:bg-gray-600 text-white font-medium py-2 px-4 rounded-lg transition duration-300 ease-in-out", className)}>
  <slot />
</a>
