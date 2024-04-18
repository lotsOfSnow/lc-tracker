<script lang="ts">
  import { type ClassName, cn } from '$lib/components/componentUtils';

  let className: ClassName = undefined;

  export { className as class };
  export let href: string;
</script>

<a href={href} {...$$restProps}
   class={cn("inline-block bg-gray-700 hover:bg-gray-600 text-white font-medium py-1 px-2 rounded transition duration-300 ease-in-out", className)}
>
  Edit
</a>
