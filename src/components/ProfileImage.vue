<script setup lang="ts">
const props = defineProps<{ url?: string; name: string }>();
const classList = 'w-40 h-40 object-cover rounded-full';
</script>

<template>
  <img :class="classList" :src="props.url" v-if="props.url" :alt="props.name" />
  <font-awesome-icon :class="classList" v-else icon="fa-solid fa-circle-user" />
</template>
