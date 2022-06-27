<script setup lang="ts">
defineProps<{
  time: string;
}>();
</script>

<template>
  <div class="flex-grow border-t border-dashed">{{ time }}</div>
</template>
